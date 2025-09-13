using Sage100.Metadata.Import.Core;
using System.Runtime.Versioning;

class Execute
{
    /// <summary>
    /// Der Einstiegspunkt der Anwendung. Initialisiert die Anwendung, verarbeitet Dateien und steuert die Benutzerinteraktion.
    /// </summary>
    /// <param name="args">Ein Array von Befehlszeilenargumenten. Das erste Argument gibt das Anwendungsverzeichnis an; falls nicht angegeben, wird das Standard-Basisverzeichnis der Anwendung verwendet.</param>
    [SupportedOSPlatform("windows")]
    static void Main(string[] args)
    {
        var appDesigner = new AppDesigner(args.Length > 0 ? args[0] : Application.BaseDirectory());
        PrintHeader(appDesigner);

        if (appDesigner.Files.Count == 0)
        {
            PrintNoFilesFound();
        }
        else
        {
            PrintFiles(appDesigner.Files);

            if (AskForImport())
            {
                ImportFiles(appDesigner);
            }
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Environment.NewLine + "Ausführung beendet, drücke eine Taste zum beenden.");
        Console.ReadLine();
    }

    /// <summary>
    /// Zeigt einen formatierten Header mit anwendungsspezifischen Pfaden und Bezeichnungen an.
    /// </summary>
    /// <remarks>Diese Methode gibt den Pfad der ausführbaren Datei und den Basispfad der Anwendung aus, gefolgt von einer Abschnittsüberschrift für Dateien. Die Ausgabe ist farblich hervorgehoben, wobei Bezeichnungen in Weiß und Pfade in Grün dargestellt werden.</remarks>
    /// <param name="appDesigner">Eine Instanz von <see cref="AppDesigner"/>, die die anzuzeigenden Anwendungspfade bereitstellt.</param>
    [SupportedOSPlatform("windows")]
    static void PrintHeader(AppDesigner appDesigner)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Importer".PadRight(15));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(appDesigner.ExecutablePath);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Ordner".PadRight(15));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(appDesigner.BasePath);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(Environment.NewLine + "Dateien");
    }

    /// <summary>
    /// Zeigt eine Meldung an, dass keine Metadatendateien gefunden wurden und der Import nicht möglich ist.
    /// </summary>
    /// <remarks>Die Meldung wird in roter Schrift angezeigt, um den Fehler hervorzuheben. Diese Methode ist für Szenarien gedacht, in denen das Fehlen von Metadatendateien die weitere Verarbeitung verhindert.</remarks>
    static void PrintNoFilesFound()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Environment.NewLine + "Keine Metadaten gefunden, kein Import möglich");
    }

    /// <summary>
    /// Gibt den Status und Namen jeder Datei in der angegebenen Sammlung auf der Konsole aus.
    /// </summary>
    /// <param name="files">Eine Sammlung von Dateien, die ausgegeben werden sollen. Der Status jeder Datei wird in Gelb, gefolgt vom Namen in Weiß angezeigt.</param>
    static void PrintFiles(ImportFiles files)
    {
        foreach (var file in files)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(file.State.ToString().PadRight(15));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(file);
        }
    }

    /// <summary>
    /// Fordert den Benutzer auf zu bestätigen, ob Metadaten importiert werden sollen.
    /// </summary>
    /// <remarks>Die Methode zeigt eine Nachricht zur Bestätigung an und liest die Eingabe von der Konsole. Gibt der Benutzer "y" (Groß-/Kleinschreibung wird ignoriert) ein, gibt die Methode <see langword="true"/> zurück; andernfalls <see langword="false"/>. Leere Zeilen werden aus der Konsole entfernt, um eine saubere Ausgabe zu gewährleisten.</remarks>
    /// <returns><see langword="true"/>, wenn der Benutzer den Import mit "y" bestätigt; andernfalls <see langword="false"/>.</returns>
    static bool AskForImport()
    {
        Console.WriteLine(Environment.NewLine + "Sollen die Metadaten importiert werden? (y = ja)");
        var answer = Console.ReadLine();

        if (!string.IsNullOrEmpty(answer))
        {
            int currentLineCursor = Console.CursorTop - 1;
            Console.SetCursorPosition(0, currentLineCursor);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        return answer?.ToLower() == "y";
    }

    /// <summary>
    /// Importiert Dateien mit der angegebenen <see cref="AppDesigner"/>-Instanz und verarbeitet deren Status.
    /// </summary>
    /// <remarks>Diese Methode iteriert über die vom <paramref name="appDesigner"/> bereitgestellten Dateien und verarbeitet jede Datei mit der Methode <see cref="AppDesigner.ExecuteFile"/>. Der Status jeder Datei wird ausgewertet und die Konsolenausgabe entsprechend dem Ergebnis aktualisiert. Die Konsolenschriftfarbe ändert sich je nach Status der Datei: Grün bei Erfolg und Rot bei Fehlern.</remarks>
    /// <param name="appDesigner">Die <see cref="AppDesigner"/>-Instanz, die die zu importierenden Dateien und die Logik zu deren Ausführung bereitstellt.</param>
    static void ImportFiles(AppDesigner appDesigner)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Environment.NewLine + "Import wird gestartet...");

        foreach (var file in appDesigner.Files)
        {
            switch (appDesigner.ExecuteFile(file).State)
            {
                case ImportFile.FileStates.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ImportFile.FileStates.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.Write(file.State.ToString().PadRight(15));
            Console.ResetColor();
            Console.WriteLine(file);
        }
    }
}