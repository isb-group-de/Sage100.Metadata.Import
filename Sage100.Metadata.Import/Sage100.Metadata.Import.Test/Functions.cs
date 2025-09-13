using Sage100.Metadata.Import.Core;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Test
{
    public class Functions
    {
        /// <summary>
        /// Überprüft, ob die Anwendung auf einem Windows-Betriebssystem läuft.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass die Methode <see cref="Application.RunOnWindows"/> <see
        /// langword="true"/> zurückgibt. Er dient zur Validierung plattformspezifischen Verhaltens für Windows-Umgebungen.</remarks>
        [Fact]
        public void IsWindowsTest() => Assert.True(Application.RunOnWindows());

        /// <summary>
        /// Überprüft, dass das Basisverzeichnis der Anwendung nicht leer ist.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass die Methode <see cref="Application.BaseDirectory"/> eine
        /// nicht-leere Zeichenkette zurückgibt, was darauf hinweist, dass das Basisverzeichnis der Anwendung korrekt ermittelt wurde.</remarks>
        [Fact]
        public void BaseDirectoryTest() => Assert.NotEmpty(Application.BaseDirectory());

        /// <summary>
        /// Testet, dass die Methode <see cref="Sage.GetRuntimePath"/> einen nicht-leeren Pfad zurückgibt.
        /// </summary>
        /// <remarks>Dieser Test überprüft, dass der Laufzeitpfad für Sage 100 korrekt ermittelt und nicht leer ist.
        /// Der Test wird nur auf Windows-Plattformen unterstützt.</remarks>
        [Fact]
        [SupportedOSPlatform("windows")]
        public void GetSage100PathTest() => Assert.NotEmpty(Sage.GetRuntimePath());

        /// <summary>
        /// Überprüft, dass die Methode <see cref="Sage.GetExecutableFullPath"/> eine nicht-leere Zeichenkette zurückgibt.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass der von <see cref="Sage.GetExecutableFullPath"/> ermittelte Pfad zur ausführbaren Datei gültig und nicht leer ist.</remarks>
        [Fact]
        public void GetAppDesignerExecutableFullPathTest() => Assert.NotEmpty(Sage.GetExecutableFullPath());
    }
}