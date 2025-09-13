using Microsoft.Win32;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Core
{
    /// <summary>
    /// Stellt Hilfsmethoden und Konstanten für die Interaktion mit Sage 100 Konfigurations- und Laufzeiteinstellungen bereit.
    /// </summary>
    /// <remarks>Diese Klasse enthält Methoden zum Abrufen von Konfigurationspfaden und zum Erstellen von Dateipfaden,
    /// die sich auf Sage 100 beziehen. Sie ist für Umgebungen konzipiert, in denen Sage 100 installiert und konfiguriert ist.
    /// Alle Methoden und Konstanten sind spezifisch für die Windows-Plattform.</remarks>
    public static class Sage
    {
        #region Constant
        /// <summary>
        /// Stellt den Registrierungspfad für Sage 100 Konfigurationseinstellungen dar.
        /// </summary>
        /// <remarks>Diese Konstante gibt den Registrierungspfad an, der zum Auffinden der Sage 100 Einstellungen
        /// für Version 9.0 verwendet wird. Sie ist für den Zugriff auf oder die Änderung von Sage 100 Konfigurationsdaten vorgesehen.</remarks>
        public const string SAGE_100_PATH = @"SOFTWARE\WOW6432Node\Sage\Office Line\9.0";
        #endregion

        #region Registry
        /// <summary>
        /// Ruft den Laufzeitpfad der Anwendung aus der Windows-Registrierung ab.
        /// </summary>
        /// <remarks>Diese Methode versucht, den Wert "Root" aus einem bestimmten Registrierungsschlüssel zu lesen.
        /// Wenn der Schlüssel oder Wert nicht existiert, gibt die Methode einen leeren String zurück.</remarks>
        /// <returns>Der Laufzeitpfad als String, wenn der Registrierungsschlüssel und Wert gefunden werden; andernfalls ein leerer String.</returns>
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
        /// <summary>
        /// Gibt den vollständigen Dateipfad der ausführbaren Datei für den Echtzeit-Datenaustausch zurück.
        /// </summary>
        /// <remarks>Der zurückgegebene Pfad wird durch die Kombination des Laufzeitpfads mit dem relativen Pfad
        /// zur ausführbaren Datei erstellt. Stellen Sie sicher, dass der Laufzeitpfad korrekt konfiguriert und zugänglich ist, bevor Sie diese Methode aufrufen.</remarks>
        /// <returns>Ein String, der den vollständigen Pfad zur ausführbaren Datei einschließlich Verzeichnis und Dateiname darstellt.</returns>
        [SupportedOSPlatform("windows")]
        public static string GetExecutableFullPath()
        {
            return Path.Combine(GetRuntimePath(), "Shared", "Sagede.Shared.RealTimeData.Metadata.Exchange.exe");
        }
        #endregion
    }
}
