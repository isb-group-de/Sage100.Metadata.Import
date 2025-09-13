using Microsoft.Win32;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Core
{
    public static class Sage
    {
        #region Constant
        public const string SAGE_100_PATH = @"SOFTWARE\WOW6432Node\Sage\Office Line\9.0";
        #endregion

        #region Registry
        [SupportedOSPlatform("windows")]
        public static string GetRuntimePath()
        {
            using (var key = Registry.LocalMachine.OpenSubKey(SAGE_100_PATH))
            {
                if (key?.GetValue("Root") is string configuredPath)
                    return configuredPath;
            }

            return string.Empty;
        }
        #endregion

        #region Path
        public static string GetExecutableFullPath()
        {
            return Path.Combine(GetRuntimePath(), "Shared", "Sagede.Shared.RealTimeData.Metadata.Exchange.exe");
        }
        #endregion
    }
}
