using Sage100.Metadata.Import.Core;

namespace Sage100.Metadata.Import.Test
{
    public class Components
    {
        /// <summary>
        /// Testet die Funktionalität der Metadatensammlung der <see cref="AppDesigner"/>-Klasse.
        /// </summary>
        /// <remarks>Dieser Test überprüft, ob die <see cref="AppDesigner"/>-Instanz korrekt initialisiert wird 
        /// und dass die <see cref="AppDesigner.Files"/>-Sammlung nach der Instanziierung mit einem gültigen
        /// Verzeichnispfad nicht leer ist.</remarks>
        [Fact]
        public void CollectMetadataTest()
        {
            var appDesigner = new AppDesigner("C:\\Temp\\Metadata");
            Assert.NotEmpty(appDesigner.Files);
        }

        /// <summary>
        /// Testet den Import von Metadatendateien, um sicherzustellen, dass alle Dateien erfolgreich verarbeitet werden.
        /// </summary>
        /// <remarks>Dieser Test überprüft, dass die Methode <see cref="AppDesigner.ExecuteAll"/> alle
        /// Metadatendateien fehlerfrei verarbeitet. Eine Datei gilt als erfolgreich verarbeitet, wenn ihr Status <see
        /// cref="ImportFile.FileStates.Success"/> ist.</remarks>
        [Fact]
        public void ImportMetadataAllTest()
        {
            var appDesigner = new AppDesigner("C:\\Temp\\Metadata");
            Assert.True(appDesigner.ExecuteAll().FindAll(x => x.State != ImportFile.FileStates.Success).Count == 0);
        }
    }
}