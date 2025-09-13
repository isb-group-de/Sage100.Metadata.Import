# Sage100.Metadata.Import

Ein leistungsfähiges Tool zum Importieren und Verarbeiten von Metadatendateien für die Sage 100.  
Das Tool unterstützt die automatisierte Erkennung, Statusanzeige und den Import von Metadaten im `.metadata`-Format.

<img width="512" height="512" alt="favicon" src="https://github.com/user-attachments/assets/e18e1e42-7103-4608-941e-3a100bdab708" />

## Features

- **Automatischer Import** von Metadatendateien aus einem angegebenen Verzeichnis
- **Statusanzeige** für jede Datei (Erfolg, Fehler, nicht initialisiert)
- **Benutzerinteraktion** zur Bestätigung des Imports
- **Farbliche Konsolenausgabe** für bessere Übersicht
- **Erweiterbar** durch modulare Architektur (Kernlogik in separaten Klassen)

## Voraussetzungen

- .NET 8.0 SDK oder höher
- Windows-Betriebssystem (wegen Registry- und Pfadzugriffen)
- Zugriff auf das Verzeichnis mit den zu importierenden `.metadata`-Dateien

## Installation

1. Repository klonen:
```
git clone https://github.com/isb-group-de/Sage100.Metadata.Import.git
```

2. Projekt mit Visual Studio 2022 oder per CLI öffnen/bauen:
```
cd Sage100.Metadata.Import dotnet build
```


## Verwendung

### Über die Konsole

Das Tool wird über die Kommandozeile gestartet. Optional kann ein Verzeichnis als Argument übergeben werden, in dem nach Metadatendateien gesucht werden soll.
```
dotnet run --project Sage100.Metadata.Import.Console [Pfad/zum/Verzeichnis]
```

- **Ohne Argument:** Es wird das Standard-Basisverzeichnis der Anwendung verwendet.
- **Mit Argument:** Das angegebene Verzeichnis wird für die Suche nach `.metadata`-Dateien genutzt.

### Ablauf

1. Das Tool zeigt die gefundenen Metadatendateien und deren Status an.
2. Der Benutzer wird gefragt, ob der Import gestartet werden soll (`y` für Ja).
3. Jede Datei wird importiert und der Status (Erfolg/Fehler) farblich angezeigt.
4. Nach Abschluss wartet das Tool auf eine Benutzereingabe zum Beenden.

## Beispielausgabe
```
Importer       C:\Program Files (x86)\Sage\Sage 100\9.0\Shared\Sagede.Shared.RealTimeData.Metadata.Exchange.exe
Ordner         C:\Temp\Metadata

Dateien
Uninitialized  C:\Temp\Metadata\100068625.ISBAPIExample.metadata
Uninitialized  C:\Temp\Metadata\100068625.ISBAPITest.metadata

Sollen die Metadaten importiert werden? (y = ja)

Import wird gestartet...
Success        C:\Temp\Metadata\100068625.ISBAPIExample.metadata
Success        C:\Temp\Metadata\100068625.ISBAPITest.metadata
```


## Projektstruktur

- **Sage100.Metadata.Import.Console**  
  Konsolenanwendung, Einstiegspunkt (`Execute.cs`)
- **Sage100.Metadata.Import.Core**  
  Kernlogik: Datei- und Importverwaltung (`AppDesigner`, `ImportFile`, `Application`, `Sage`)
- **Sage100.Metadata.Import.Test**  
  Unit- und Integrationstests

## Erweiterung

Die Importlogik ist in der Klasse `AppDesigner` gekapselt und kann leicht erweitert werden, z.B. für weitere Dateitypen oder Importstrategien.

## Lizenz

Dieses Projekt steht unter der GPL-3.0 license.

## Mitwirken

Pull Requests und Issues sind willkommen! Bitte beschreibe Fehler oder Vorschläge möglichst detailliert.

---

**Kontakt:**  
[ISB Solutions GmbH](https://www.isb-solutions.de/)  
Entwickler: [Martin Rosbund](mailto:martin.rosbund@isb-solutions.de)

