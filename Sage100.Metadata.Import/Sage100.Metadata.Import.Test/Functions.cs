using Sage100.Metadata.Import.Core;
using System.Runtime.Versioning;

namespace Sage100.Metadata.Import.Test
{
    public class Functions
    {
        /// <summary>
        /// �berpr�ft, ob die Anwendung auf einem Windows-Betriebssystem l�uft.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass die Methode <see cref="Application.RunOnWindows"/> <see
        /// langword="true"/> zur�ckgibt. Er dient zur Validierung plattformspezifischen Verhaltens f�r Windows-Umgebungen.</remarks>
        [Fact]
        public void IsWindowsTest() => Assert.True(Application.RunOnWindows());

        /// <summary>
        /// �berpr�ft, dass das Basisverzeichnis der Anwendung nicht leer ist.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass die Methode <see cref="Application.BaseDirectory"/> eine
        /// nicht-leere Zeichenkette zur�ckgibt, was darauf hinweist, dass das Basisverzeichnis der Anwendung korrekt ermittelt wurde.</remarks>
        [Fact]
        public void BaseDirectoryTest() => Assert.NotEmpty(Application.BaseDirectory());

        /// <summary>
        /// Testet, dass die Methode <see cref="Sage.GetRuntimePath"/> einen nicht-leeren Pfad zur�ckgibt.
        /// </summary>
        /// <remarks>Dieser Test �berpr�ft, dass der Laufzeitpfad f�r Sage 100 korrekt ermittelt und nicht leer ist.
        /// Der Test wird nur auf Windows-Plattformen unterst�tzt.</remarks>
        [Fact]
        [SupportedOSPlatform("windows")]
        public void GetSage100PathTest() => Assert.NotEmpty(Sage.GetRuntimePath());

        /// <summary>
        /// �berpr�ft, dass die Methode <see cref="Sage.GetExecutableFullPath"/> eine nicht-leere Zeichenkette zur�ckgibt.
        /// </summary>
        /// <remarks>Dieser Test stellt sicher, dass der von <see cref="Sage.GetExecutableFullPath"/> ermittelte Pfad zur ausf�hrbaren Datei g�ltig und nicht leer ist.</remarks>
        [Fact]
        public void GetAppDesignerExecutableFullPathTest() => Assert.NotEmpty(Sage.GetExecutableFullPath());
    }
}