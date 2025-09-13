namespace Sage100.Metadata.Import.Core
{
    /// <summary>
    /// Stellt eine Sammlung von zu importierenden Dateien dar, wobei jede Datei durch ein <see cref="ImportFile"/>
    /// Objekt repräsentiert wird.
    /// </summary>
    /// <remarks>Diese Klasse erbt von <see cref="List{T}"/> und bietet somit die gesamte Funktionalität einer generischen
    /// Liste zur Verwaltung von <see cref="ImportFile"/>-Objekten. Sie kann verwendet werden, um eine Menge von zu verarbeitenden
    /// Dateien zu speichern, zu bearbeiten und zu durchlaufen.</remarks>
    public class ImportFiles : List<ImportFile>
    {

    }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="ImportFile"/>-Klasse mit dem angegebenen Dateinamen.
    /// </summary>
    /// <param name="fileName">Der Name der zu importierenden Datei. Darf nicht null oder leer sein.</param>
    public class ImportFile(string fileName)
    {
        #region Property
        /// <summary>
        /// Ruft den Namen der Datei ab oder legt ihn fest.
        /// </summary>
        public string FileName { get; set; } = fileName;
        /// <summary>
        /// Ruft den aktuellen Status der Datei ab oder legt ihn fest.
        /// </summary>
        public FileStates State { get; set; } = FileStates.Uninitialized;
        #endregion

        #region Enum
        /// <summary>
        /// Stellt die möglichen Zustände einer Datei während der Verarbeitung dar.
        /// </summary>
        /// <remarks>Diese Enumeration wird typischerweise verwendet, um das Ergebnis von dateibezogenen
        /// Operationen anzuzeigen.</remarks>
        public enum FileStates
        {
            Uninitialized,
            Success,
            Error
        }
        #endregion

        #region ToString 
        /// <summary>
        /// Gibt eine Zeichenfolgen-Darstellung des aktuellen Objekts zurück.
        /// </summary>
        /// <returns>Den Wert der <see cref="FileName"/>-Eigenschaft.</returns>
        public override string ToString()
        {
            return FileName;
        }
        #endregion
    }
}
