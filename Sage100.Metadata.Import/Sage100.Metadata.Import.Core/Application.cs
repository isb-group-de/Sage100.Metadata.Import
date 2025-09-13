using System.Runtime.InteropServices;

namespace Sage100.Metadata.Import.Core
{
    public static class Application
    {
        #region Windows
        public static bool RunOnWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        #endregion

        #region Windows
        public static string BaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        #endregion
    }
}
