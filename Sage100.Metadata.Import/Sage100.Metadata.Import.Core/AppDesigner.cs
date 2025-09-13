using System.Diagnostics;

namespace Sage100.Metadata.Import.Core
{
    public class AppDesigner
    {
        #region Constant
        public const string SAGE_100_PATH = @"SOFTWARE\WOW6432Node\Sage\Office Line\9.0";
        #endregion

        #region Property
        public ImportFiles Files { get; set; }
        public string BasePath { get; set; }
        public string ExecutablePath => Sage.GetExecutableFullPath();
        #endregion

        #region Construct        
        public AppDesigner()
        {
            BasePath = Application.BaseDirectory();
            Files = GetMetadataFiles();
        }

        public AppDesigner(string basePath)
        {
            BasePath = basePath;
            Files = GetMetadataFiles();
        }
        #endregion

        #region Execute
        public ImportFile ExecuteFile(ImportFile file)
        {
            var arguments = string.Join("", "/action=import /inputfile=", "\"", file.FileName, "\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = ExecutablePath,
                    Arguments = arguments,
                    Verb = "runas"
                }
            };

            bool result = process.Start();

            if (result)
            {
                file.State = ImportFile.FileStates.Success;
                process.WaitForExit();
            }
            else
            {
                file.State = ImportFile.FileStates.Error;
            }

            return file;
        }

        public ImportFiles ExecuteAll(bool cancelOnError = false)
        {
            foreach (var filename in Files)
            {
                if (ExecuteFile(filename).State == ImportFile.FileStates.Error && cancelOnError)
                {
                    break;
                }
            }
            return Files;
        }
        #endregion

        #region Files    
        public ImportFiles GetMetadataFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = new ImportFiles();

            try
            {
                foreach (string filename in Directory.GetFiles(BasePath, "*.metadata", searchOption))
                {
                    files.Add(new ImportFile(filename));
                }
            }
            catch
            {
                files.Clear();
            }

            return files;
        }
        #endregion
    }
}
