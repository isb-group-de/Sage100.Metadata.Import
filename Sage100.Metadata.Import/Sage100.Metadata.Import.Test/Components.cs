using Sage100.Metadata.Import.Core;

namespace Sage100.Metadata.Import.Test
{
    public class Components
    {
        [Fact]
        public void CollectMetadataTest()
        {
            var appDesigner = new AppDesigner("C:\\Temp\\Metadata");
            Assert.NotEmpty(appDesigner.Files);
        }

        [Fact]
        public void ImportMetadataAllTest()
        {
            var appDesigner = new AppDesigner("C:\\Temp\\Metadata");
            Assert.True(appDesigner.ExecuteAll().FindAll(x => x.State != ImportFile.FileStates.Success).Count == 0);
        }
    }
}