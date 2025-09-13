using System.Diagnostics;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Core
{
    public class AppDesigner
    {
        #region Constant
        /// <summary>
        /// Registrierungspfad für Sage 100.
        /// </summary>
        public const string SAGE_100_PATH = @"SOFTWARE\WOW6432Node\Sage\Office Line\9.0";
        #endregion

        #region Property
        /// <summary>
        /// Ruft die zu importierenden Dateien ab oder legt sie fest.
        /// </summary>
        public ImportFiles Files { get; set; }
        /// <summary>
        /// Ruft den Basis-Pfad für Datei- oder Verzeichnisoperationen ab oder legt ihn fest.
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Ruft den vollständigen Pfad zur ausführbaren Datei der aktuellen Anwendung ab.
        /// </summary>
        /// <remarks>Diese Eigenschaft wird nur auf Windows-Plattformen unterstützt.</remarks>
        [SupportedOSPlatform("windows")]
        public string ExecutablePath => Sage.GetExecutableFullPath();
        #endregion

        #region Construct        
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AppDesigner"/>-Klasse.
        /// </summary>
        /// <remarks>Dieser Konstruktor setzt den <see cref="BasePath"/> auf das Basisverzeichnis der Anwendung
        /// und initialisiert die <see cref="Files"/>-Sammlung mit Metadatendateien.</remarks>
        public AppDesigner()
        {
            BasePath = Application.BaseDirectory();
            Files = GetMetadataFiles();
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AppDesigner"/>-Klasse mit dem angegebenen Basis-Pfad.
        /// </summary>
        /// <remarks>Der Konstruktor initialisiert die <see cref="BasePath"/>-Eigenschaft mit dem angegebenen
        /// <paramref name="basePath"/> und ruft Metadatendateien über die <c>GetMetadataFiles</c>-Methode ab.</remarks>
        /// <param name="basePath">Das Basisverzeichnis, das zum Auffinden der Metadatendateien verwendet wird. Dieser Wert darf nicht null oder leer sein.</param>
        public AppDesigner(string basePath)
        {
            BasePath = basePath;
            Files = GetMetadataFiles();
        }
        #endregion

        /// <summary>
        /// Führt die angegebene Importdatei mit einem externen Prozess aus und aktualisiert deren Status basierend auf dem Ergebnis.
        /// </summary>
        /// <remarks>Diese Methode startet einen externen Prozess, um den Importvorgang auszuführen. Der Prozess
        /// wird mit erhöhten Rechten (über das "runas"-Verb) ausgeführt. Die <see cref="ImportFile.State"/>-Eigenschaft wird auf
        /// <see cref="ImportFile.FileStates.Success"/> gesetzt, wenn der Prozess erfolgreich gestartet und abgeschlossen wurde.
        /// Falls der Prozess nicht gestartet werden kann, wird der Status auf <see cref="ImportFile.FileStates.Error"/> gesetzt.</remarks>
        /// <param name="file">Die zu verarbeitende <see cref="ImportFile"/>. Die <see cref="ImportFile.FileName"/>-Eigenschaft muss
        /// den Pfad zur zu importierenden Datei angeben.</param>
        /// <returns>Die gleiche <see cref="ImportFile"/>-Instanz mit aktualisierter <see cref="ImportFile.State"/>-Eigenschaft,
        /// die das Ergebnis des Vorgangs angibt.</returns>
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

        /// <summary>
        /// Führt den Importprozess für alle Dateien in der Sammlung aus.
        /// </summary>
        /// <remarks>Jede Datei in der Sammlung wird nacheinander verarbeitet. Wenn <paramref
        /// name="cancelOnError"/> auf <see langword="true"/> gesetzt ist und während der Ausführung einer Datei ein Fehler auftritt,
        /// wird die Verarbeitung weiterer Dateien gestoppt. Der Status jeder Datei wird entsprechend dem Ergebnis aktualisiert.</remarks>
        /// <param name="cancelOnError">Gibt an, ob die Ausführung bei einem Fehler abgebrochen werden soll. Wenn <see langword="true"/>,
        /// wird die Ausführung beim Auftreten eines Fehlers gestoppt, andernfalls werden alle Dateien verarbeitet.</param>
        /// <returns>Die Sammlung der Dateien nach der Ausführung, mit aktualisierten Statuswerten entsprechend dem Ergebnis des Importprozesses.</returns>
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
        /// <summary>
        /// Ruft Metadatendateien aus dem angegebenen Verzeichnis und dessen Unterverzeichnissen ab, basierend auf der Suchoption.
        /// </summary>
        /// <remarks>Metadatendateien werden anhand der Dateiendung "*.metadata" identifiziert. Die Methode versucht,
        /// alle passenden Dateien im angegebenen Verzeichnis und dessen Unterverzeichnissen entsprechend der <paramref
        /// name="searchOption"/> zu finden. Tritt während der Dateisuche eine Ausnahme auf, wird eine leere Sammlung zurückgegeben.</remarks>
        /// <param name="searchOption">Gibt an, ob nur das oberste Verzeichnis oder alle Unterverzeichnisse durchsucht werden sollen. Standard ist <see
        /// cref="SearchOption.TopDirectoryOnly"/>.</param>
        /// <returns>Eine <see cref="ImportFiles"/>-Sammlung mit den gefundenen Metadatendateien. Bei einem Fehler während der Suche wird eine leere Sammlung zurückgegeben.</returns>
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
