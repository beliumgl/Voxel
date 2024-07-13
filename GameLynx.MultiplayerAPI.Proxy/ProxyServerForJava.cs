using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameLynx.MultiplayerAPI.Proxy;

public class ProxyServerForJava
{
    public const string MulticastGroup = "224.0.2.60";

    public const int PingPort = 4445;

    private const long PingInterval = 1500L;

    private readonly string motd;

    private readonly string remoteServerAddress;

    private readonly int remoteServerPort;

    private readonly UdpClient udpClient;

    private TcpListener tcpListener;

    private TcpClient minecraftServerTcpClient;

    private NetworkStream minecraftServerStream;

    private TcpClient minecraftClientTcpClient;

    private NetworkStream minecraftClientStream;

    public static int randomPort;

    private Thread pingThread;

    private Thread clientToServerThread;

    private Thread serverToClientThread;

    private static bool isOld;

    public ProxyServerForJava(string motd, string remoteServerAddress, int remoteServerPort, bool isOldProtocol = false)
    {
        this.motd = motd;
        this.remoteServerAddress = remoteServerAddress;
        isOld = isOldProtocol;
        this.remoteServerPort = remoteServerPort;
        randomPort = new Random().Next(1, 65535);
        udpClient = new UdpClient();
    }

    public void Start(CancellationToken t)
    {
        pingThread = new Thread((ThreadStart)delegate
        {
            PingThread(t);
        })
        {
            IsBackground = true
        };
        pingThread.Start();
        tcpListener = new TcpListener(IPAddress.Any, randomPort);
        tcpListener.Start();
        while (!t.IsCancellationRequested)
        {
            minecraftClientTcpClient = tcpListener.AcceptTcpClient();
            minecraftClientStream = minecraftClientTcpClient.GetStream();
            minecraftServerTcpClient = new TcpClient(remoteServerAddress, remoteServerPort);
            minecraftServerStream = minecraftServerTcpClient.GetStream();
            clientToServerThread = new Thread(ClientToServerThread)
            {
                IsBackground = true
            };
            clientToServerThread.Start();
            serverToClientThread = new Thread(ServerToClientThread)
            {
                IsBackground = true
            };
            serverToClientThread.Start();
            clientToServerThread.Join();
            serverToClientThread.Join();
            minecraftClientStream.Close();
            minecraftClientTcpClient.Close();
        }
    }

    private void PingThread(CancellationToken t)
    {
        try
        {
            string s = PayloadCrt(motd);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("224.0.2.60"), 4445);
            while (!t.IsCancellationRequested)
            {
                udpClient.Send(bytes, bytes.Length, endPoint);
                Thread.Sleep(1500);
            }
        }
        catch (Exception)
        {
        }
    }

    private void ClientToServerThread()
    {
        try
        {
            byte[] array = new byte[1024];
            int count;
            while ((count = minecraftClientStream.Read(array, 0, array.Length)) > 0)
            {
                minecraftServerStream.Write(array, 0, count);
            }
        }
        catch (Exception)
        {
        }
    }

    private void ServerToClientThread()
    {
        try
        {
            byte[] array = new byte[1024];
            int count;
            while ((count = minecraftServerStream.Read(array, 0, array.Length)) > 0)
            {
                minecraftClientStream.Write(array, 0, count);
            }
        }
        catch (Exception)
        {
        }
    }

    public static string PayloadCrt(string motd)
    {
        if (!isOld)
        {
            return $"[MOTD]{motd}[/MOTD][AD]{randomPort}[/AD]";
        }
        return $"[MOTD]{motd}[/MOTD][AD]127.0.0.1:{randomPort}[/AD]";
    }
}
