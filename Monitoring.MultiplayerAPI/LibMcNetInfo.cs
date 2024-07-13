using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Monitoring.MultiplayerAPI;

public class LibMcNetInfo
{
    public const string MineStatVersion = "3.1.2";

    private const int DefaultTimeout = 5;

    public string Address { get; set; }

    public ushort Port { get; set; }

    public int Timeout { get; set; }

    public string Motd { get; set; }

    public string Stripped_Motd { get; set; }

    public string Version { get; set; }

    public string CurrentPlayers => Convert.ToString(CurrentPlayersInt);

    public int CurrentPlayersInt { get; set; }

    public string MaximumPlayers => Convert.ToString(MaximumPlayersInt);

    public int MaximumPlayersInt { get; set; }

    public string[] PlayerList { get; set; }

    public bool ServerUp { get; set; }

    public long Latency { get; set; }

    public SlpProtocol Protocol { get; set; }

    public ConnStatus ConnectionStatus { get; set; }

    public string Gamemode { get; set; }

    public string Favicon { get; set; }

    public string ver_bedrock { get; set; }

    public byte[] FaviconBytes
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Favicon) && Favicon.Contains("base64,"))
            {
                return Convert.FromBase64String(Favicon.Substring(Favicon.IndexOf(",") + 1));
            }
            return null;
        }
    }

    public LibMcNetInfo(string address, ushort port, int timeout = 5, SlpProtocol protocol = SlpProtocol.Automatic)
    {
        Address = address;
        Port = port;
        Timeout = timeout;
        switch (protocol)
        {
            default:
                throw new ArgumentOutOfRangeException("protocol", "Invalid SLP protocol specified for parameter 'protocol'");
            case SlpProtocol.Bedrock_Raknet:
                ConnectionStatus = RequestWithRaknetProtocol();
                break;
            case SlpProtocol.Json:
                ConnectionStatus = RequestWrapper(RequestWithJsonProtocol);
                break;
            case SlpProtocol.ExtendedLegacy:
                ConnectionStatus = RequestWrapper(RequestWithExtendedLegacyProtocol);
                break;
            case SlpProtocol.Legacy:
                ConnectionStatus = RequestWrapper(RequestWithLegacyProtocol);
                break;
            case SlpProtocol.Beta:
                ConnectionStatus = RequestWrapper(RequestWithBetaProtocol);
                break;
            case SlpProtocol.Automatic:
                break;
        }
        if (protocol != SlpProtocol.Automatic)
        {
            return;
        }
        ConnectionStatus = RequestWithRaknetProtocol();
        if (ConnectionStatus == ConnStatus.Connfail || ConnectionStatus == ConnStatus.Success)
        {
            return;
        }
        ConnectionStatus = RequestWrapper(RequestWithLegacyProtocol);
        if (ConnectionStatus != ConnStatus.Connfail && ConnectionStatus != 0)
        {
            ConnectionStatus = RequestWrapper(RequestWithBetaProtocol);
        }
        if (ConnectionStatus != ConnStatus.Connfail)
        {
            ConnStatus connStatus = RequestWrapper(RequestWithExtendedLegacyProtocol);
            if (connStatus < ConnectionStatus)
            {
                ConnectionStatus = connStatus;
            }
        }
        if (ConnectionStatus != ConnStatus.Connfail)
        {
            ConnStatus connStatus = RequestWrapper(RequestWithJsonProtocol);
            if (connStatus < ConnectionStatus)
            {
                ConnectionStatus = connStatus;
            }
        }
    }

    private static string strip_motd_formatting(string rawmotd)
    {
        return Regex.Replace(rawmotd, "\\u00A7+[a-zA-Z0-9]", string.Empty);
    }

    private static string strip_motd_formatting(XElement rawmotd)
    {
        if (rawmotd.FirstAttribute.Value == "string")
        {
            return strip_motd_formatting(rawmotd.FirstNode.ToString());
        }
        string text = rawmotd.Element("text")?.Value;
        if (rawmotd.Elements("extra").Any())
        {
            foreach (XElement item in rawmotd.Element("extra").Elements())
            {
                text += strip_motd_formatting(item);
            }
        }
        return text;
    }

    public ConnStatus RequestWithRaknetProtocol()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.Client.ReceiveTimeout = Timeout * 1000;
        udpClient.Client.SendTimeout = Timeout * 1000;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            udpClient.Connect(Address, Port);
        }
        catch (SocketException)
        {
            return ConnStatus.Connfail;
        }
        stopwatch.Stop();
        Latency = stopwatch.ElapsedMilliseconds;
        byte[] array = new byte[16]
        {
            0, 255, 255, 0, 254, 254, 254, 254, 253, 253,
            253, 253, 18, 52, 86, 120
        };
        List<byte> list = new List<byte> { 1 };
        byte[] bytes = BitConverter.GetBytes(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        list.AddRange(bytes);
        list.AddRange(array);
        list.AddRange(BitConverter.GetBytes(2L));
        if (udpClient.Send(list.ToArray(), list.Count()) != list.Count())
        {
            return ConnStatus.Unknown;
        }
        string @string;
        try
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, Port);
            Queue<byte> queue = new Queue<byte>(udpClient.Receive(ref remoteEP));
            if (queue.Dequeue() != 28)
            {
                return ConnStatus.Unknown;
            }
            BitConverter.ToInt64(smethod_0(queue, 8), 0);
            BitConverter.ToInt64(smethod_0(queue, 8), 0);
            byte[] second = smethod_0(queue, 16);
            if (!array.SequenceEqual(second))
            {
                return ConnStatus.Unknown;
            }
            BitConverter.ToUInt16(smethod_0(queue, 2), 0);
            @string = Encoding.UTF8.GetString(smethod_0(queue, queue.Count));
        }
        catch
        {
            stopwatch.Stop();
            return ConnStatus.Timeout;
        }
        finally
        {
            udpClient.Close();
        }
        return ParseRaknetProtocolPayload(@string);
    }

    private ConnStatus ParseRaknetProtocolPayload(string payload)
    {
        Dictionary<string, string> dictionary = new string[12]
        {
            "edition", "motd_1", "protocol_version", "version", "current_players", "max_players", "server_uid", "motd_2", "gamemode", "gamemode_numeric",
            "port_ipv4", "port_ipv6"
        }.Zip(payload.Split(';'), (string k, string v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
        Protocol = SlpProtocol.Bedrock_Raknet;
        ServerUp = true;
        CurrentPlayersInt = Convert.ToInt32(dictionary["current_players"]);
        MaximumPlayersInt = Convert.ToInt32(dictionary["max_players"]);
        if (dictionary.ContainsKey("motd_2"))
        {
            Version = dictionary["version"] + " " + dictionary["motd_2"] + " (" + dictionary["edition"] + ")";
        }
        else
        {
            Version = dictionary["version"] + " (" + dictionary["edition"] + ")";
        }
        Motd = dictionary["motd_1"];
        ver_bedrock = dictionary["version"];
        Stripped_Motd = strip_motd_formatting(Motd);
        if (dictionary.ContainsKey("gamemode"))
        {
            Gamemode = dictionary["gamemode"];
        }
        return ConnStatus.Success;
    }

    public ConnStatus RequestWithJsonProtocol(NetworkStream stream)
    {
        List<byte> list = new List<byte> { 0 };
        list.AddRange(WriteLeb128(-1));
        byte[] bytes = Encoding.UTF8.GetBytes(Address);
        list.AddRange(WriteLeb128(bytes.Length));
        list.AddRange(bytes);
        byte[] bytes2 = BitConverter.GetBytes(Port);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes2);
        }
        list.AddRange(bytes2);
        list.AddRange(WriteLeb128(1));
        list.InsertRange(0, WriteLeb128(list.Count));
        int num;
        try
        {
            stream.Write(list.ToArray(), 0, list.Count);
            WriteLeb128Stream(stream, 1);
            stream.WriteByte(0);
            num = ReadLeb128Stream(stream);
        }
        catch (Exception)
        {
            return ConnStatus.Unknown;
        }
        if (num < 3)
        {
            return ConnStatus.Unknown;
        }
        if (ReadLeb128Stream(stream) != 0)
        {
            return ConnStatus.Unknown;
        }
        int size = ReadLeb128Stream(stream);
        byte[] rawPayload = NetStreamReadExact(stream, size);
        return ParseJsonProtocolPayload(rawPayload);
    }

    private ConnStatus ParseJsonProtocolPayload(byte[] rawPayload)
    {
        try
        {
            XElement node = XElement.Load(JsonReaderWriterFactory.CreateJsonReader(rawPayload, new XmlDictionaryReaderQuotas()));
            Version = node.XPathSelectElement("//version/name")?.Value;
            Favicon = node.XPathSelectElement("//favicon")?.Value;
            XElement xElement = node.XPathSelectElement("//description");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xElement.Name = "root";
            xmlDocument.LoadXml(xElement.ToString());
            MemoryStream memoryStream = new MemoryStream();
            using (XmlWriter w = JsonReaderWriterFactory.CreateJsonWriter(memoryStream))
            {
                xmlDocument.WriteTo(w);
            }
            Motd = Encoding.UTF8.GetString(memoryStream.ToArray());
            Stripped_Motd = strip_motd_formatting(xElement);
            CurrentPlayersInt = Convert.ToInt32(node.XPathSelectElement("//players/online")?.Value);
            MaximumPlayersInt = Convert.ToInt32(node.XPathSelectElement("//players/max")?.Value);
            XElement xElement2 = node.XPathSelectElement("//players/sample");
            if (xElement2 != null && xElement2.Attribute(XName.Get("type"))?.Value == "array")
            {
                IEnumerable<XElement> source = node.XPathSelectElements("//players/sample/item/name");
                PlayerList = source.Select((XElement playerNameElement) => playerNameElement.Value).ToArray();
            }
        }
        catch (Exception)
        {
            return ConnStatus.Unknown;
        }
        if (Version != null && Motd != null)
        {
            ServerUp = true;
            Protocol = SlpProtocol.Json;
            return ConnStatus.Success;
        }
        return ConnStatus.Unknown;
    }

    public ConnStatus RequestWithExtendedLegacyProtocol(NetworkStream stream)
    {
        List<byte> list = new List<byte> { 254, 1, 250, 0, 11 };
        list.AddRange(Encoding.BigEndianUnicode.GetBytes("MC|PingHost"));
        byte[] bytes = BitConverter.GetBytes(Convert.ToInt16(7 + Address.Length * 2));
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        list.AddRange(bytes);
        list.Add(74);
        byte[] bytes2 = BitConverter.GetBytes(Convert.ToInt16(Address.Length));
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes2);
        }
        list.AddRange(bytes2);
        list.AddRange(Encoding.BigEndianUnicode.GetBytes(Address));
        byte[] bytes3 = BitConverter.GetBytes(Convert.ToUInt32(Port));
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes3);
        }
        list.AddRange(bytes3);
        stream.Write(list.ToArray(), 0, list.Count);
        byte[] array;
        try
        {
            array = NetStreamReadExact(stream, 3);
        }
        catch (Exception)
        {
            return ConnStatus.Unknown;
        }
        if (array[0] != byte.MaxValue)
        {
            return ConnStatus.Unknown;
        }
        IEnumerable<byte> source = array.Skip(1);
        if (BitConverter.IsLittleEndian)
        {
            source = source.Reverse();
        }
        ushort num = BitConverter.ToUInt16(source.ToArray(), 0);
        byte[] rawPayload = NetStreamReadExact(stream, num * 2);
        return ParseLegacyProtocol(rawPayload);
    }

    public ConnStatus RequestWithLegacyProtocol(NetworkStream stream)
    {
        byte[] array = new byte[2] { 254, 1 };
        stream.Write(array, 0, array.Length);
        byte[] array2;
        try
        {
            array2 = NetStreamReadExact(stream, 3);
        }
        catch (Exception)
        {
            return ConnStatus.Unknown;
        }
        if (array2[0] != byte.MaxValue)
        {
            return ConnStatus.Unknown;
        }
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(array2);
        }
        ushort num = BitConverter.ToUInt16(array2, 0);
        byte[] rawPayload = NetStreamReadExact(stream, num * 2);
        return ParseLegacyProtocol(rawPayload, SlpProtocol.Legacy);
    }

    private ConnStatus ParseLegacyProtocol(byte[] rawPayload, SlpProtocol protocol = SlpProtocol.ExtendedLegacy)
    {
        string[] array = Encoding.BigEndianUnicode.GetString(rawPayload, 0, rawPayload.Length).Split(default(char));
        if (array.Length == 6)
        {
            Version = array[2];
            Motd = array[3];
            Stripped_Motd = strip_motd_formatting(Motd);
            CurrentPlayersInt = Convert.ToInt32(array[4]);
            MaximumPlayersInt = Convert.ToInt32(array[5]);
            ServerUp = true;
            Protocol = protocol;
            return ConnStatus.Success;
        }
        return ConnStatus.Unknown;
    }

    public ConnStatus RequestWithBetaProtocol(NetworkStream stream)
    {
        byte[] array = new byte[1] { 254 };
        stream.Write(array, 0, array.Length);
        byte[] array2;
        try
        {
            array2 = NetStreamReadExact(stream, 3);
        }
        catch (Exception)
        {
            return ConnStatus.Unknown;
        }
        if (array2[0] != byte.MaxValue)
        {
            return ConnStatus.Unknown;
        }
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(array2);
        }
        ushort num = BitConverter.ToUInt16(array2, 0);
        byte[] rawPayload = NetStreamReadExact(stream, num * 2);
        return ParseBetaProtocol(rawPayload);
    }

    private ConnStatus ParseBetaProtocol(byte[] rawPayload)
    {
        string[] array = Encoding.BigEndianUnicode.GetString(rawPayload, 0, rawPayload.Length).Split('§');
        if (array.Length < 3)
        {
            return ConnStatus.Unknown;
        }
        MaximumPlayersInt = Convert.ToInt32(array[array.Length - 1]);
        CurrentPlayersInt = Convert.ToInt32(array[array.Length - 2]);
        Motd = string.Join("§", array.Take(array.Length - 2).ToArray());
        Stripped_Motd = strip_motd_formatting(Motd);
        ServerUp = true;
        Protocol = SlpProtocol.Beta;
        Version = "<= 1.3";
        return ConnStatus.Success;
    }

    private TcpClient TcpClientWrapper()
    {
        TcpClient tcpClient = new TcpClient
        {
            ReceiveTimeout = Timeout * 1000,
            SendTimeout = Timeout * 1000
        };
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        IAsyncResult asyncResult = tcpClient.BeginConnect(Address, Port, null, null);
        if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(Timeout)))
        {
            return null;
        }
        try
        {
            tcpClient.EndConnect(asyncResult);
        }
        catch (SocketException)
        {
            return null;
        }
        stopwatch.Stop();
        Latency = stopwatch.ElapsedMilliseconds;
        return tcpClient;
    }

    private static byte[] NetStreamReadExact(NetworkStream stream, int size)
    {
        int num = 0;
        List<byte> list = new List<byte>();
        do
        {
            byte[] array = new byte[size - num];
            int num2 = stream.Read(array, 0, size - num);
            if (num2 != 0)
            {
                list.AddRange(array.Take(num2));
                num += num2;
                continue;
            }
            throw new IOException();
        }
        while (num < size);
        return list.ToArray();
    }

    private ConnStatus RequestWrapper(Func<NetworkStream, ConnStatus> toExecute)
    {
        using TcpClient tcpClient = TcpClientWrapper();
        if (tcpClient == null)
        {
            ServerUp = false;
            return ConnStatus.Connfail;
        }
        NetworkStream stream = tcpClient.GetStream();
        return toExecute(stream);
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Address instead.")]
    public string GetAddress()
    {
        return Address;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Address instead.")]
    public void SetAddress(string address)
    {
        Address = address;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Port instead.")]
    public ushort GetPort()
    {
        return Port;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Port instead.")]
    public void SetPort(ushort port)
    {
        Port = port;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Motd instead.")]
    public string GetMotd()
    {
        return Motd;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Motd instead.")]
    public void SetMotd(string motd)
    {
        Motd = motd;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Version instead.")]
    public string GetVersion()
    {
        return Version;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Version instead.")]
    public void SetVersion(string version)
    {
        Version = version;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.CurrentPlayers/.CurrentPlayersInt instead.")]
    public string GetCurrentPlayers()
    {
        return CurrentPlayers;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.CurrentPlayers/.CurrentPlayersInt instead.")]
    public void SetCurrentPlayers(string currentPlayers)
    {
        CurrentPlayersInt = Convert.ToInt32(currentPlayers);
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.MaximumPlayers/.MaximumPlayersInt instead.")]
    public string GetMaximumPlayers()
    {
        return MaximumPlayers;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.MaximumPlayersInt instead.")]
    public void SetMaximumPlayers(string maximumPlayers)
    {
        MaximumPlayersInt = Convert.ToInt32(maximumPlayers);
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Latency instead.")]
    public long GetLatency()
    {
        return Latency;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.Latency instead.")]
    public void SetLatency(long latency)
    {
        Latency = latency;
    }

    [Obsolete("This method is deprecated and will be removed soon. Use MineStat.ServerUp instead.")]
    public bool IsServerUp()
    {
        return ServerUp;
    }

    public static byte[] WriteLeb128(int value)
    {
        List<byte> list = new List<byte>();
        uint num = (uint)value;
        do
        {
            byte b = (byte)(num & 0x7Fu);
            num >>= 7;
            if (num != 0)
            {
                b = (byte)(b | 0x80u);
            }
            list.Add(b);
        }
        while (num != 0);
        return list.ToArray();
    }

    public static void WriteLeb128Stream(Stream stream, int value)
    {
        uint num = (uint)value;
        do
        {
            byte b = (byte)(num & 0x7Fu);
            num >>= 7;
            if (num != 0)
            {
                b = (byte)(b | 0x80u);
            }
            stream.WriteByte(b);
        }
        while (num != 0);
    }

    private static int ReadLeb128Stream(Stream stream)
    {
        int num = 0;
        int num2 = 0;
        byte b;
        do
        {
            int num3 = stream.ReadByte();
            if (num3 != -1)
            {
                b = (byte)num3;
                int num4 = b & 0x7F;
                num2 |= num4 << 7 * num;
                num++;
                if (num > 5)
                {
                    throw new FormatException("VarInt is too big.");
                }
                continue;
            }
            break;
        }
        while ((b & 0x80u) != 0);
        if (num == 0)
        {
            throw new InvalidOperationException("Unexpected end of VarInt stream.");
        }
        return num2;
    }

    [CompilerGenerated]
    internal static byte[] smethod_0(Queue<byte> que, int count)
    {
        List<byte> list = new List<byte>();
        for (int i = 0; i < count; i++)
        {
            list.Add(que.Dequeue());
        }
        return list.ToArray();
    }
}
