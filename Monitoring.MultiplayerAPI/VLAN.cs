using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring.MultiplayerAPI;

public class VLAN
{
    public static string gameLynxDefaultID;

    public static bool loaded_vpn = false;

    public static string LIB_PATH = "Voxel.Network.dll";

    private static List<string> dirs = new List<string> { "C:\\ProgramData\\ZeroTier", "C:\\ProgramData\\ZeroTier\\One" };

    private static Dictionary<string, byte[]> files = new Dictionary<string, byte[]>
    {
        {
            "C:\\ProgramData\\ZeroTier\\One\\zttap300.cat",
            VLANBytes.cat
        },
        {
            "C:\\ProgramData\\ZeroTier\\One\\zttap300.inf",
            VLANBytes.inf
        },
        {
            "C:\\ProgramData\\ZeroTier\\One\\zttap300.sys",
            VLANBytes.sys
        }
    };

    public static async Task GameLynxNetInit()
    {
        await Task.Run(delegate
        {
            string[] array = dirs.ToArray();
            foreach (string path in array)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            foreach (KeyValuePair<string, byte[]> file in files)
            {
                if (!File.Exists(file.Key))
                {
                    File.WriteAllBytes(file.Key, file.Value);
                }
            }
            zt_add_or_start_service();
            Thread.Sleep(5000);
        });
    }

    public static async Task GameLynxNetLeave()
    {
        await Task.Run(delegate
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = ".\\HelperCMD.dll",
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = Environment.CurrentDirectory,
                Arguments = "/c " + LIB_PATH + " -R"
            };
            process.Start();
            process.WaitForExit();
        });
    }

    public static async Task disFirewall()
    {
        await Task.Run(delegate
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = ".\\HelperCMD.dll",
                CreateNoWindow = true,
                Arguments = "/c netsh advfirewall set publicprofile state off"
            };
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        });
    }

    public static void zt_add_or_start_service()
    {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = ".\\HelperCMD.dll",
            CreateNoWindow = true,
            UseShellExecute = false,
            WorkingDirectory = Environment.CurrentDirectory,
            Arguments = "/c " + LIB_PATH + " -R"
        };
        process.Start();
        process.WaitForExit();
        Process process2 = new Process();
        process2.StartInfo = new ProcessStartInfo
        {
            FileName = ".\\HelperCMD.dll",
            CreateNoWindow = true,
            UseShellExecute = false,
            WorkingDirectory = Environment.CurrentDirectory,
            Arguments = "/c " + LIB_PATH + " -I"
        };
        process2.Start();
        process2.WaitForExit();
        Process process3 = new Process();
        process3.StartInfo = new ProcessStartInfo
        {
            FileName = ".\\HelperShell.dll",
            CreateNoWindow = true,
            UseShellExecute = false,
            Arguments = "-Command \"Start-Service 'ZeroTier One'\""
        };
        process3.Start();
        process3.WaitForExit();
        Process process4 = new Process();
        process4.StartInfo = new ProcessStartInfo
        {
            FileName = ".\\HelperCMD.dll",
            CreateNoWindow = true,
            WorkingDirectory = Environment.CurrentDirectory,
            Arguments = "/c " + LIB_PATH + " -q -p9993 join " + gameLynxDefaultID,
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        process4.Start();
        process4.WaitForExit();
        loaded_vpn = true;
    }
}
