using GameLynx;
using GameLynx.AccountSystem;
using Monitoring.MultiplayerAPI;
using Monitoring.UI;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;

namespace Monitoring;

public static class VoxelMC
{
    public static BedrockServer[] servers_ = null;

    public static JavaWorld[] worlds_ = null;

    public static string networkStartsWith;

    private static Thread _PROCESS;

    public static int selServer = 0;

    public static string PATH_TO_MINECRAFTUWP_COMMOJANG = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Packages\\Microsoft.MinecraftUWP_8wekyb3d8bbwe\\LocalState\\games\\com.mojang";

    public static string PATH_TO_MINECRAFTUWP = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Packages\\Microsoft.MinecraftUWP_8wekyb3d8bbwe";

    public static string MINECRAFT_PATH = "shell:AppsFolder\\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";

    public static string getVoxelNetworkAdress()
    {
        IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        foreach (IPAddress iPAddress in addressList)
        {
            if (iPAddress.AddressFamily == AddressFamily.InterNetwork && iPAddress.ToString().StartsWith(networkStartsWith))
            {
                return iPAddress.ToString();
            }
        }
        return "NULL";
    }

    public static void Init()
    {
        try
        {
            parameters obj = JsonConvert.DeserializeObject<parameters>(BackendConnect.getBackendResponse(v.BACKEND + "/network/get_params.php").Result);
            VLAN.gameLynxDefaultID = obj.nwid;
            networkStartsWith = obj.ipbase;
        }
        catch
        {
            new GMessageBoxOK("Ошибка! Возможно, сервер Voxel Multiplayer сейчас не активен, попробуйте позже.").ShowDialog();
            Application.Exit();
        }
    }

    public static string getLanAdress()
    {
        IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        foreach (IPAddress iPAddress in addressList)
        {
            if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                return iPAddress.ToString();
            }
        }
        return "NULL";
    }

    public static string algHashEncode(string plainText, string keyString)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(keyString);
        using Aes aes = Aes.Create();
        using ICryptoTransform transform = aes.CreateEncryptor(bytes, aes.IV);
        using MemoryStream memoryStream = new MemoryStream();
        memoryStream.Write(BitConverter.GetBytes(aes.IV.Length), 0, 4);
        memoryStream.Write(aes.IV, 0, aes.IV.Length);
        using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
        {
            using StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(plainText);
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string algHashDecode(string cipherText, string keyString)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);
        byte[] bytes = Encoding.UTF8.GetBytes(keyString);
        using Aes aes = Aes.Create();
        using MemoryStream memoryStream = new MemoryStream(buffer);
        aes.IV = ReadByteArray(memoryStream);
        using ICryptoTransform transform = aes.CreateDecryptor(bytes, aes.IV);
        using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    private static byte[] ReadByteArray(Stream s)
    {
        byte[] array = new byte[4];
        if (s.Read(array, 0, array.Length) != array.Length)
        {
            throw new SystemException("Stream did not contain properly formatted byte array");
        }
        byte[] array2 = new byte[BitConverter.ToInt32(array, 0)];
        if (s.Read(array2, 0, array2.Length) != array2.Length)
        {
            throw new SystemException("Did not read byte array properly");
        }
        return array2;
    }

    public static string getHashPass(string input)
    {
        return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input)));
    }

    public static string getSize(byte[] bytes)
    {
        int num = bytes.Length;
        string arg = "B";
        if (num > 1024)
        {
            num /= 1024;
            arg = "KB";
            if (num > 1024)
            {
                num /= 1024;
                arg = "MB";
                if (num >= 1000)
                {
                    num /= 1000;
                    arg = "GB";
                }
            }
        }
        return $"{num} {arg}";
    }

    public static string getSizeForInt(long bytes)
    {
        long num = bytes;
        string arg = "B";
        if (num > 1024L)
        {
            num /= 1024L;
            arg = "KB";
            if (num > 1024L)
            {
                num /= 1024L;
                arg = "MB";
                if (num >= 1000L)
                {
                    num /= 1000L;
                    arg = "GB";
                }
            }
        }
        return $"{num} {arg}";
    }

    public static void changeMultiplayerName()
    {
        if (Directory.Exists(PATH_TO_MINECRAFTUWP) && File.Exists(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\options.txt"))
        {
            try
            {
                string[] array = File.ReadAllLines(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\options.txt");
                array[0] = "mp_username:§l§tGL §r" + Acc.Name;
                File.WriteAllLines(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\options.txt", array);
            }
            catch
            {
            }
        }
    }

    public static void Play(string IP, int PORT, string NAME)
    {
        _PROCESS = new Thread((ThreadStart)delegate
        {
            PlayOnServer(IP, PORT, NAME);
        });
        _PROCESS.Start();
    }

    public static void runMinecraft()
    {
        if (Process.GetProcessesByName("Minecraft.Windows").Length != 0)
        {
            Process.GetProcessesByName("Minecraft.Windows")[0].Kill();
        }
        if (Process.GetProcessesByName("RuntimeBroker").Length != 0)
        {
            Process.GetProcessesByName("RuntimeBroker")[0].Kill();
        }
        Thread.Sleep(1000);
        Process.Start(new ProcessStartInfo
        {
            FileName = MINECRAFT_PATH
        });
    }

    public static void PlayOnServer(string ip, int port, string name)
    {
        try
        {
            bool flag = false;
            _ = Environment.UserName;
            if (!File.Exists(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\external_servers.txt"))
            {
                File.Create(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\external_servers.txt");
            }
            string[] array = File.ReadAllText(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\external_servers.txt").Split('\n');
            for (int i = 0; i <= array.Length - 1; i++)
            {
                if (Find(array[i].Split(':'), ip) && Find(array[i].Split(':'), port.ToString()))
                {
                    flag = true;
                    break;
                }
            }
            string text = File.ReadAllText(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\external_servers.txt");
            if (!flag)
            {
                File.WriteAllText(PATH_TO_MINECRAFTUWP_COMMOJANG + "\\minecraftpe\\external_servers.txt", text + $"\n{new Random().Next(21, 1223112)}:§l§s§oVoxel§r {new string(name.Where((char c) => c != ':').ToArray())}:{ip}:{port}:0");
            }
            changeMultiplayerName();
            runMinecraft();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ERROR: " + ex.Message);
        }
    }

    public static bool Find(string[] str, string Find)
    {
        bool result = false;
        for (int i = 0; i <= str.Length - 1; i++)
        {
            if (str[i] == Find)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
