namespace Voxel.Types;

public class JavaWorld
{
    public string id { get; set; }

    public string ip { get; set; }

    public string port { get; set; }

    public string userID { get; set; }

    public string UserName { get; set; }

    public bool PassIsEnabled { get; set; }

    public string PassString { get; set; }

    public int IsOldProtocol { get; set; }
}
