using Monitoring;
using Monitoring.ServersList;
using Monitoring.UI;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel;
using Voxel.Types;

namespace GameLynx.MultiplayerAPI.Java;

internal class McJavaLanGameFinder
{
    private readonly UdpClient socket;

    private readonly IPEndPoint pingGroup;

    public McJavaLanGameFinder()
    {
        try
        {
            socket = new UdpClient(4445);
            pingGroup = new IPEndPoint(IPAddress.Parse("224.0.2.60"), 4445);
            socket.JoinMulticastGroup(pingGroup.Address);
        }
        catch
        {
            new GMessageBoxOK("Не удалось начать поиск мира, попробуйте закрыть Minecraft, открыть его и не заходя в раздел \"Сетевая игра\" зайти в свой мир, открыть его для сети и повторить попытку снова. Если это окно повторно вывелось, попробуйте перезапустить Voxel Multiplayer.").ShowDialog();
            try
            {
                StopSocket();
            }
            catch
            {
            }
            JavaWorld[] worlds = BackendConnect.updateJavaWorldsList().Result;
            Thread thread = new Thread((ThreadStart)delegate
            {
                Application.Run(new javaMultiplayer(worlds));
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Application.Exit();
        }
    }

    public async Task<string[]> SearchAsync(CancellationToken token)
    {
        try
        {
            string locapIp = VoxelMC.getLanAdress();
            new IPEndPoint(IPAddress.Any, 0);
            using (token.Register(delegate
            {
                socket.Close();
            }))
            {
                UdpReceiveResult udpReceiveResult = await socket.ReceiveAsync();
                byte[] buffer = udpReceiveResult.Buffer;
                string @string = Encoding.UTF8.GetString(buffer);
                if (@string.Contains("[MOTD]") && @string.Contains("[AD]") && udpReceiveResult.RemoteEndPoint.Address.ToString() == locapIp)
                {
                    return parsePayload(@string);
                }
                return await SearchAsync(token);
            }
        }
        catch
        {
            return new string[2];
        }
    }

    public void StopSocket()
    {
        socket.Close();
    }

    public string[] parsePayload(string payload)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        int num = payload.IndexOf("[MOTD]");
        string text = payload.Substring(length: payload.IndexOf("[/MOTD]", num + "[MOTD]".Length) - num - "[MOTD]".Length, startIndex: num + "[MOTD]".Length);
        dictionary.Add("motd", text);
        int num2 = payload.IndexOf("[AD]", num + "[/MOTD]".Length);
        int num3 = payload.IndexOf("[/AD]", num2 + "[AD]".Length);
        string text2 = payload.Substring(num2 + "[AD]".Length, num3 - num2 - "[AD]".Length);
        return new string[2] { text, text2 };
    }
}
