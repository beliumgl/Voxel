using GameLynx.AccountSystem;
using Monitoring.UI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace GameLynx;

internal static class Program
{
    private static Mutex mutex;

    [STAThread]
    private static void Main()
    {
        mutex = new Mutex(initiallyOwned: true, "Voxel Multiplayer", out var createdNew);
        if (createdNew)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            string text = new WebClient().DownloadString(v.API_CENTRAL + "/download/api").Replace("\n", "");
            string text2 = v.version[0] + "." + v.version[1] + "." + v.version[2];
            if (text != text2)
            {
                new GMessageBoxOK("Эта версия программы устарела. Скачайте новую с официального сайта Voxel Multiplayer!").ShowDialog();
                return;
            }
            if (File.Exists(v.ACC_PATH))
            {
                try
                {
                    AccLog accLog = JsonConvert.DeserializeObject<AccLog>(File.ReadAllText(v.ACC_PATH));
                    Acc.Name = accLog.AccName;
                    Acc.Service = Convert.ToInt32(accLog.AccServ);
                    Acc.ID = accLog.AccID;
                    Acc.Avatar = Image.FromStream(new MemoryStream(Convert.FromBase64String(accLog.AccAvContent)));
                    Application.Run(new App());
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Run(new auth());
                    return;
                }
            }
            Application.Run(new auth());
        }
        else
        {
            new GMessageBoxOK("Приложение уже запущено.").ShowDialog();
            Application.Exit();
        }
    }
}

