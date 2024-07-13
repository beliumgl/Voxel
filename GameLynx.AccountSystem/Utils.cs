using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Voxel.Types;

namespace GameLynx.AccountSystem;

public class Utils
{
    public static Dictionary<int, string> SERVICES = new Dictionary<int, string>
    {
        { 0, "GitHub" },
        { 1, "Google" }
    };

    public static byte[] getBytesImage(Image img)
    {
        MemoryStream memoryStream = new MemoryStream();
        img.Save(memoryStream, ImageFormat.Png);
        return memoryStream.ToArray();
    }

    public static string GetQueryStringParameter(string queryString, string parameterName)
    {
        string[] array = queryString.TrimStart('?').Split('&');
        int num = 0;
        string[] array2;
        while (true)
        {
            if (num < array.Length)
            {
                array2 = array[num].Split('=');
                if (array2.Length == 2 && array2[0] == parameterName)
                {
                    break;
                }
                num++;
                continue;
            }
            return null;
        }
        return array2[1];
    }

    public static int[] getUserWorks(BedrockServer[] servers, string ID)
    {
        if (servers != null && !string.IsNullOrEmpty(ID))
        {
            List<int> list = new List<int>();
            for (int i = 0; i <= servers.Length - 1; i++)
            {
                if (servers[i].AccountServiceID == ID)
                {
                    list.Add(i);
                }
            }
            return list.ToArray();
        }
        return new int[0];
    }

    public static string searchNameService(int index)
    {
        return SERVICES[index];
    }
}
