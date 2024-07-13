using DiscordRPC;
using DiscordRPC.Logging;
using GameLynx;
using System;
using System.Windows.Forms;
using Button = DiscordRPC.Button;

namespace Monitoring;

public class rpcAPI
{
    public static bool isStarted;

    public static void RPC_INIT()
    {
        try
        {
            DiscordRpcClient val = new DiscordRpcClient(Voxel.Properties.Settings.Default.qajctkZL4IeE);
            val.Logger = (ILogger)new ConsoleLogger
            {
                Level = (LogLevel)3
            };
            val.Initialize();
            RichPresence val2 = new RichPresence();
            ((BaseRichPresence)val2).State = "Сейчас в мультиплеере для Minecraft.";
            val2.Buttons = (Button[])(object)new Button[2]
            {
                new Button
                {
                    Label = "ДС сервер проекта",
                    Url = "https://discord.gg/5wWpYEeHVV"
                },
                new Button
                {
                    Label = "Сайт проекта",
                    Url = v.API_CENTRAL
                }
            };
            ((BaseRichPresence)val2).Assets = new Assets
            {
                LargeImageKey = "gamelynx_rpc"
            };
            val.SetPresence(val2);
            val.Invoke();
            isStarted = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
