using System.Runtime.InteropServices;

namespace Sage100.Metadata.Import.Core
{
    /// <summary>
    /// Stellt Hilfsmethoden zum Abrufen anwendungsspezifischer Informationen und zur Bestimmung des Betriebssystems bereit.
    /// </summary>
    /// <remarks>Diese Klasse enthält statische Methoden zur Unterstützung der Plattform-Erkennung und zum Abrufen des Anwendungsverzeichnisses. Sie ist für Szenarien konzipiert, in denen plattformspezifisches Verhalten oder Anwendungsverzeichnispfade benötigt werden.</remarks>
    public static class Application
    {
        #region Windows
        /// <summary>
        /// Bestimmt, ob das aktuelle Betriebssystem Windows ist.
        /// </summary>
        /// <remarks>Diese Methode prüft die Laufzeitumgebung, um festzustellen, ob die Anwendung auf einer Windows-Plattform ausgeführt wird.</remarks>
        /// <returns><see langword="true"/>, wenn das aktuelle Betriebssystem Windows ist; andernfalls <see langword="false"/>.</returns>
        public static bool RunOnWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        #endregion

        #region Windows
        /// <summary>
        /// Gibt das Basisverzeichnis zurück, aus dem die Anwendung ausgeführt wird.
        /// </summary>
        /// <remarks>Diese Methode ruft das Basisverzeichnis der Anwendung ab, das in der Regel das Verzeichnis mit der ausführbaren Datei der Anwendung ist. Der zurückgegebene Pfad ist vollständig qualifiziert und endet mit einem Verzeichnistrenner.</remarks>
        /// <returns>Der Basisverzeichnispfad der aktuellen Anwendungsdomäne.</returns>
        public static string BaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        #endregion
    }
}
