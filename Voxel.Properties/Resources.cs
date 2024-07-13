using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Voxel.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[CompilerGenerated]
[DebuggerNonUserCode]
internal class Resources
{
    private static ResourceManager resourceMan;

    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
        get
        {
            if (resourceMan == null)
            {
                resourceMan = new ResourceManager("Voxel.Properties.Resources", typeof(Resources).Assembly);
            }
            return resourceMan;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
        set
        {
            resourceCulture = value;
        }
    }

    internal static Bitmap _150px_AV => (Bitmap)ResourceManager.GetObject("150px_AV", resourceCulture);

    internal static Bitmap _50px_DS => (Bitmap)ResourceManager.GetObject("_50px_DS", resourceCulture);

    internal static Bitmap _50px_SITE => (Bitmap)ResourceManager.GetObject("_50px_SITE", resourceCulture);

    internal static Bitmap _50px_TG => (Bitmap)ResourceManager.GetObject("_50px_TG", resourceCulture);

    internal static Bitmap _50px_YT => (Bitmap)ResourceManager.GetObject("_50px_YT", resourceCulture);

    internal static Bitmap bad_ping => (Bitmap)ResourceManager.GetObject("bad_ping", resourceCulture);

    internal static Bitmap cloud => (Bitmap)ResourceManager.GetObject("cloud", resourceCulture);

    internal static Bitmap exit => (Bitmap)ResourceManager.GetObject("exit", resourceCulture);

    internal static Bitmap gamelynx => (Bitmap)ResourceManager.GetObject("gamelynx", resourceCulture);

    internal static Bitmap low_ping => (Bitmap)ResourceManager.GetObject("low_ping", resourceCulture);

    internal static Bitmap mcJava => (Bitmap)ResourceManager.GetObject("mcJava", resourceCulture);

    internal static Bitmap mcJava_dark => (Bitmap)ResourceManager.GetObject("mcJava_dark", resourceCulture);

    internal static Bitmap minecraft => (Bitmap)ResourceManager.GetObject("minecraft", resourceCulture);

    internal static Bitmap minecraft_dark => (Bitmap)ResourceManager.GetObject("minecraft_dark", resourceCulture);

    internal static Bitmap norm_ping => (Bitmap)ResourceManager.GetObject("norm_ping", resourceCulture);

    internal static Bitmap ok_ping => (Bitmap)ResourceManager.GetObject("ok_ping", resourceCulture);

    internal static Bitmap onlineGame => (Bitmap)ResourceManager.GetObject("onlineGame", resourceCulture);

    internal static Bitmap title => (Bitmap)ResourceManager.GetObject("title", resourceCulture);

    internal static Bitmap ultra_bad_ping => (Bitmap)ResourceManager.GetObject("ultra_bad_ping", resourceCulture);

    internal static Bitmap unknown_ping => (Bitmap)ResourceManager.GetObject("unknown_ping", resourceCulture);

    internal Resources()
    {
    }
}
