using GameLynx;
using Monitoring;
using Monitoring.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voxel.Types;

namespace Voxel;

public class BackendConnect
{
    public static Dictionary<string, string> def_params = new Dictionary<string, string> {
    {
        "key",
        Voxel.Properties.Settings.Default.Gtm8XVOhghwEc45gi1Y14HdUYhoSV7
    } };

    public static async Task<string> getBackendResponse(string url, Dictionary<string, string> parameters = null)
    {
        try
        {
            if (parameters == null)
            {
                parameters = def_params;
            }
            HttpClient val = new HttpClient();
            FormUrlEncodedContent val2 = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)parameters);
            string text = await (await val.PostAsync(url, (HttpContent)(object)val2)).Content.ReadAsStringAsync();
            if (!(text != "UNKNOWN_KEY"))
            {
                new GMessageBoxOK("Доступ запрещён.").ShowDialog();
                Application.Exit();
                return null;
            }
            return text;
        }
        catch
        {
            new GMessageBoxOK("Ошибка подключения. Проверьте подключение к интернету.").ShowDialog();
            return null;
        }
    }

    public static async Task<JavaWorld[]> updateJavaWorldsList()
    {
        return VoxelMC.worlds_ = JsonConvert.DeserializeObject<JavaWorld[]>(await getBackendResponse(v.BACKEND + "/get_java_local_worlds.php"));
    }

    public static async Task<BedrockServer[]> updateBedrockServersList()
    {
        return VoxelMC.servers_ = JsonConvert.DeserializeObject<BedrockServer[]>(await getBackendResponse(v.BACKEND + "/get_bedrock_servers.php"));
    }
}
