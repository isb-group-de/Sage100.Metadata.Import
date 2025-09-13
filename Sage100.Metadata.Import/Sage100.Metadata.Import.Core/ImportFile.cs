namespace Sage100.Metadata.Import.Core
{
    public class ImportFiles : List<ImportFile>
    {

    }

    public class ImportFile
    {
        #region Property
        public string FileName { get; set; }
        public FileStates State { get; set; } = FileStates.Uninitialized;
        #endregion

        #region Enum
        public enum FileStates
        {
            Uninitialized,
            Success,
            Error
        }
        #endregion

        #region Construct        
        public ImportFile(string fileName)
        {
            FileName = fileName;
        }
        #endregion

        #region ToString 
        public override string ToString()
        {
            return FileName;
        }
        #endregion
    }
}
