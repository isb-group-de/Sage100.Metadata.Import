using Sage100.Metadata.Import.Core;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Test
{
    public class Functions
    {
        [Fact]
        public void IsWindowsTest() => Assert.True(Application.RunOnWindows());

        [Fact]
        public void BaseDirectoryTest() => Assert.NotEmpty(Application.BaseDirectory());
        
        [Fact]
        [SupportedOSPlatform("windows")]
        public void GetSage100PathTest() => Assert.NotEmpty(Sage.GetRuntimePath());

        [Fact]
        public void GetAppDesignerExecutableFullPathTest() => Assert.NotEmpty(Sage.GetExecutableFullPath());
    }
}