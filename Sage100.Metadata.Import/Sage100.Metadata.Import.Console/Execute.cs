using Sage100.Metadata.Import.Core;

class Execute
{
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

    static void PrintNoFilesFound()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Environment.NewLine + "Keine Metadaten gefunden, kein Import möglich");
    }

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

    static bool AskForImport()
    {
        Console.WriteLine(Environment.NewLine + "Sollen die Metadaten importiert werden?");
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