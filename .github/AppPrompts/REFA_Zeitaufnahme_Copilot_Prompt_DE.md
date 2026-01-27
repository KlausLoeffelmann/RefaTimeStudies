# Copilot-Prompt: REFA-Zeitaufnahme-Anwendung

## Übersicht

Erstelle eine Windows Forms-Anwendung in C# (.NET 9 oder höher) zur Durchführung von REFA-Zeitaufnahmen. Die Anwendung ist für den Einsatz auf Touch-fähigen Geräten wie dem Microsoft Surface Go 3 konzipiert.

Die Anwendung muss eine **vollständige Deutsch/Englisch-Lokalisierung** mittels Standard-WinForms-Ressourcendateien (.resx) unterstützen, wobei die Terminologie strikt den REFA-Standards folgt (siehe Terminologietabelle unten).

---

## Kernkonzepte und Terminologie

Die Anwendung behandelt folgende REFA-Konzepte. Verwende exakt diese Begriffe in der jeweiligen Sprache:

| Konzept | Deutscher Begriff (UI) | Englischer Begriff (UI) | Beschreibung |
|---------|------------------------|-------------------------|--------------|
| Zeitaufnahme-Definition | Zeitaufnahme-Definition | Time Study Definition | Vorlage, die definiert, welche Ablaufabschnitte gemessen werden |
| Zeitaufnahme | Zeitaufnahme | Time Study | Eine tatsächliche Aufnahmesitzung basierend auf einer Definition |
| Ablaufabschnitt | Ablaufabschnitt | Process Step | Ein einzelnes messbares Ablaufelement |
| Standard-Ablaufabschnitt | Standard-Ablaufabschnitt | Default Process Step | Erfasst Zeit, die keinem spezifischen Abschnitt zugeordnet ist |
| Ist-Zeit | Ist-Zeit | Actual Time | Die gemessene/beobachtete Zeit |
| Fortschrittszeit | Fortschrittszeit | Progressive Time | Kumulierte Zeit seit Beginn der Aufnahme |
| Unterbrechung durch Zeitnehmer | Unterbrechung durch Zeitnehmer (UZA) | Analyst Interruption | Pause, die durch den Zeitnehmer verursacht wird |
| Persönliche Verteilzeit | Persönliche Verteilzeit | Personal Allowance | Zeit für persönliche Bedürfnisse |
| Sachliche Verteilzeit | Sachliche Verteilzeit | Process Allowance | Zeit für prozessbedingte Unterbrechungen |
| Auftragsnummer | Auftragsnummer | Order Number | Referenznummer für einen Ablaufabschnitt |
| Aufnahme starten | Aufnahme starten | Start Recording | Zeitaufnahme beginnen |
| Aufnahme stoppen | Aufnahme stoppen | Stop Recording | Zeitaufnahme beenden |
| Unterbrechen | Unterbrechen | Pause | Vorübergehend zum Standard-Ablaufabschnitt wechseln |
| Stück | Stück | Piece | Das zu produzierende/verarbeitende Objekt |

---

## Anwendungsstruktur

### 1. Hauptfenster

Das Hauptfenster sollte enthalten:

- Eine **Menüleiste** mit:
  - Datei: Neue Definition, Definition öffnen, Definition speichern, Definition speichern unter, Zeitaufnahme exportieren (CSV), Beenden
  - Extras: Einstellungen
  - Hilfe: Über

- Eine **Symbolleiste** mit großen, touch-freundlichen Schaltflächen (mindestens 48x48 Pixel) für:
  - Neue Definition
  - Definition öffnen
  - Definition speichern
  - Aufnahme starten/stoppen
  - CSV exportieren

- Einen **Hauptinhaltsbereich**, der zwischen folgenden Ansichten wechselt:
  - Definitions-Editor (beim Erstellen/Bearbeiten von Definitionen)
  - Aufnahme-Ansicht (beim Durchführen einer Zeitaufnahme)
  - Ergebnis-Ansicht (beim Überprüfen abgeschlossener Aufnahmen)

---

### 2. Zeitaufnahme-Definition

Eine Definition besteht aus:

```csharp
public class TimeStudyDefinition
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsLocked { get; set; } // True, wenn Aufnahmen existieren
    public List<ProcessStepDefinition> ProcessSteps { get; set; }
    public int DefaultProcessStepOrderNumber { get; set; } // Welcher Abschnitt ist der Standard
}

public class ProcessStepDefinition
{
    public int OrderNumber { get; set; } // Benutzerdefiniert, z.B. 1, 2, 3... oder 18, 19, 20
    public string Description { get; set; }
    public string ProductDescription { get; set; } // Was wird verarbeitet
    public DimensionType DimensionType { get; set; } // Gewicht, Anzahl, Länge, etc.
    public string DimensionUnit { get; set; } // kg, Stück, Meter, etc.
    public decimal DefaultDimensionValue { get; set; }
    public bool IsDefaultStep { get; set; } // Markiert den "Auffang"-Abschnitt
}

public enum DimensionType
{
    Weight,    // Gewicht
    Count,     // Anzahl / Stück
    Length,    // Länge
    Area,      // Fläche
    Volume,    // Volumen
    Custom     // Benutzerdefiniert
}
```

**Funktionen des Definitions-Editors:**

- DataGridView oder ähnliches zur Bearbeitung von Ablaufabschnitten
- Möglichkeit, Abschnitte hinzuzufügen, zu entfernen und neu zu ordnen
- Kennzeichnung genau eines Abschnitts als Standard-Ablaufabschnitt
- Auftragsnummern sollten benutzerdefinierbar sein (kein Auto-Inkrement), da Zeitnehmer oft konsistente Nummerierungsschemata verwenden (z.B. 18 = Sachliche Verteilzeit, 19 = Unterbrechung durch Zeitnehmer, 20 = Persönliche Verteilzeit)
- Lücken in der Nummerierung sind erlaubt (1, 2, 5, 18, 19 ist gültig)

**Sperrverhalten:**

- Sobald eine Zeitaufnahme gegen eine Definition aufgezeichnet wurde, wird diese Definition **gesperrt** (`IsLocked = true`)
- Gesperrte Definitionen können nicht bearbeitet werden
- Option "Kopie erstellen" bereitstellen, um eine gesperrte Definition zur Bearbeitung zu kopieren

---

### 3. Zeitaufnahme-Durchführung

Die Aufnahme-Ansicht sollte für Touch-Bedienung optimiert sein:

```csharp
public class TimeStudyRecording
{
    public Guid Id { get; set; }
    public Guid DefinitionId { get; set; }
    public string DefinitionName { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsCompleted { get; set; }
    public List<TimeEntry> Entries { get; set; }
}

public class TimeEntry
{
    public int SequenceNumber { get; set; } // Auto-inkrementierend
    public int ProcessStepOrderNumber { get; set; }
    public string ProcessStepDescription { get; set; }
    public DateTime Timestamp { get; set; }
    public TimeSpan ElapsedFromStart { get; set; } // Fortschrittszeit
    public TimeSpan Duration { get; set; } // Berechnet, wenn nächster Eintrag erfolgt
    public decimal? DimensionValue { get; set; } // Überschreibung für diesen Eintrag
}
```

**Benutzeroberfläche der Aufnahme-Ansicht:**

- Große **Aufnahme starten**-Schaltfläche oben
- Große **Aufnahme stoppen**-Schaltfläche (deaktiviert bis zum Start)
- **Unterbrechen**-Schaltfläche, die zum Standard-Ablaufabschnitt wechselt
- **Ablaufabschnitt-Schaltflächen**: Ein Raster großer, touch-freundlicher Schaltflächen (mindestens 60x60 Pixel), eine für jeden definierten Ablaufabschnitt, mit Anzeige von:
  - Auftragsnummer prominent
  - Kurzbeschreibung
  - Visuelle Rückmeldung bei aktuellem Abschnitt
- **Echtzeit-Anzeige** mit:
  - Aktuelle Fortschrittszeit
  - Aktuell aktiver Ablaufabschnitt
  - Anzahl der Einträge

**Aufnahmelogik:**

1. Benutzer klickt "Aufnahme starten" - Uhr startet, Zeit läuft auf Standard-Ablaufabschnitt
2. Benutzer klickt eine Ablaufabschnitt-Schaltfläche - zeichnet Zeitstempel auf, berechnet Dauer des vorherigen Abschnitts
3. Beim Klicken auf "Unterbrechen" - wechselt zum Standard-Ablaufabschnitt (identisch mit Klick auf dessen Schaltfläche)
4. Benutzer klickt "Aufnahme stoppen" - letzter Eintrag wird gemacht, Aufnahme wird als abgeschlossen markiert

**Wichtig:** Zeit fließt immer irgendwohin. Wenn der Zeitnehmer keinen spezifischen Abschnitt anklickt, akkumuliert die Zeit auf dem Standard-Ablaufabschnitt. Dies entspricht dem beschriebenen Arbeitsablauf, bei dem undefinierte Zeiträume als "leere" Zeiten auf dem Standardabschnitt erscheinen.

---

### 4. CSV-Export

Exportiere abgeschlossene Zeitaufnahmen mit dieser Struktur:

```csv
"Definition";"Aufnahme-ID";"Gestartet";"Beendet"
"Mangel Großteile";"abc-123";"2025-11-29 14:30:00";"2025-11-29 15:45:00"

"Lfd.";"Nr.";"Beschreibung";"Zeitstempel";"Fortschrittszeit";"Dauer (s)";"Dauer (min)";"Dimensionswert"
1;40;"Unterbrechung durch Zeitnehmer";"2025-11-29 14:30:00";"00:00:00";0;0,00;
2;1;"Container holen";"2025-11-29 14:30:15";"00:00:15";15;0,25;1
3;2;"Wäsche einklammern";"2025-11-29 14:30:45";"00:00:45";30;0,50;12
...
```

Zusätzlich einen **Zusammenfassungsbereich** in der CSV bereitstellen:

```csv
"ZUSAMMENFASSUNG"
"Nr.";"Beschreibung";"Anzahl";"Gesamtdauer (s)";"Gesamtdauer (min)";"Ø Dauer (s)"
1;"Container holen";5;75;1,25;15,0
2;"Wäsche einklammern";42;1260;21,0;30,0
...
```

---

### 5. Einstellungen-Dialog (Extras > Einstellungen)

Die Einstellungen sollten umfassen:

```csharp
public class ApplicationSettings
{
    public string UiLanguage { get; set; } // "de-DE" oder "en-US"
    public string BaseDirectory { get; set; } // Wo Definitionen und Aufnahmen gespeichert werden
    public int ButtonSize { get; set; } // Minimale Schaltflächengröße in Pixeln (Standard 60)
    public bool ConfirmBeforeClosingRecording { get; set; }
    public bool AutoSaveRecordings { get; set; }
}
```

**Einstellungen-Oberfläche:**

- **Sprache**: Dropdown mit "Deutsch" und "English"
  - Sprachwechsel sollte zum Neustart der Anwendung auffordern
- **Basisverzeichnis**: Ordnerauswahl für den Speicherort von JSON-Definitionen und Aufnahmen
- **Schaltflächengröße**: Schieberegler oder numerische Eingabe
- **Automatisch speichern**: Kontrollkästchen

Einstellungen speichern unter `%AppData%\REFATimeStudy\settings.json`

---

### 6. Dateispeicherung

Verwende JSON-Format für Definitionen und Aufnahmen:

**Definitionsdateien:** `{Basisverzeichnis}\Definitionen\{Name}_{Id}.json`
**Aufnahmedateien:** `{Basisverzeichnis}\Aufnahmen\{Definitionsname}_{AufnahmeId}_{Datum}.json`

---

## Lokalisierungsanforderungen

Erstelle Ressourcendateien:
- `Resources.resx` (Standard/Englisch)
- `Resources.de.resx` (Deutsch)

Alle UI-Zeichenketten müssen aus Ressourcen stammen. Wichtige einzuschließende Zeichenketten:

| Ressourcen-Schlüssel | Englisch | Deutsch |
|---------------------|----------|---------|
| MenuFile | File | Datei |
| MenuFileNew | New Definition | Neue Definition |
| MenuFileOpen | Open Definition | Definition öffnen |
| MenuFileSave | Save Definition | Definition speichern |
| MenuFileSaveAs | Save Definition As... | Definition speichern unter... |
| MenuFileExportCsv | Export Time Study (CSV) | Zeitaufnahme exportieren (CSV) |
| MenuFileExit | Exit | Beenden |
| MenuTools | Tools | Extras |
| MenuToolsSettings | Settings | Einstellungen |
| MenuHelp | Help | Hilfe |
| MenuHelpAbout | About | Über |
| BtnStartRecording | Start Recording | Aufnahme starten |
| BtnStopRecording | Stop Recording | Aufnahme stoppen |
| BtnPause | Pause | Unterbrechen |
| LblProgressiveTime | Progressive Time | Fortschrittszeit |
| LblCurrentStep | Current Step | Aktueller Ablaufabschnitt |
| LblDefinitionName | Definition Name | Definitionsname |
| LblDescription | Description | Beschreibung |
| LblOrderNumber | Order # | Nr. |
| LblProcessStep | Process Step | Ablaufabschnitt |
| LblDefaultStep | Default Step | Standard-Ablaufabschnitt |
| LblProduct | Product | Produkt |
| LblDimension | Dimension | Dimension |
| LblDimensionType | Dimension Type | Dimensionsart |
| LblDimensionUnit | Unit | Einheit |
| LblDefaultValue | Default Value | Standardwert |
| DimWeight | Weight | Gewicht |
| DimCount | Count | Anzahl |
| DimLength | Length | Länge |
| DimArea | Area | Fläche |
| DimVolume | Volume | Volumen |
| DimCustom | Custom | Benutzerdefiniert |
| SettingsLanguage | Language | Sprache |
| SettingsBaseDir | Base Directory | Basisverzeichnis |
| SettingsButtonSize | Button Size | Schaltflächengröße |
| SettingsAutoSave | Auto-save recordings | Aufnahmen automatisch speichern |
| MsgDefinitionLocked | This definition is locked because recordings exist. Create a copy to edit. | Diese Definition ist gesperrt, da Aufnahmen existieren. Erstellen Sie eine Kopie zum Bearbeiten. |
| MsgConfirmStop | Stop the current recording? | Aktuelle Aufnahme beenden? |
| MsgUnsavedChanges | You have unsaved changes. Save before closing? | Sie haben ungespeicherte Änderungen. Vor dem Schließen speichern? |
| MsgRestartRequired | Please restart the application for language changes to take effect. | Bitte starten Sie die Anwendung neu, damit die Sprachänderungen wirksam werden. |
| TitleCreateCopy | Create Copy | Kopie erstellen |
| TitleSelectFolder | Select Base Directory | Basisverzeichnis auswählen |

---

## Technische Anforderungen

1. **Ziel-Framework:** .NET 9.0 Windows (oder .NET 8.0 LTS)
2. **Touch-freundlich:** Alle interaktiven Elemente mindestens 48x48 Pixel, Ablaufabschnitt-Schaltflächen 60x60 oder größer
3. **High-DPI-fähig:** Korrekte DPI-Skalierung verwenden
4. **Reaktionsschnelle Zeitmessung:** `System.Diagnostics.Stopwatch` für präzise Zeitmessung verwenden, nicht DateTime
5. **Thread-Sicherheit:** Timer-Aktualisierungen während der Aufnahme müssen thread-sicher sein mit korrektem UI-Thread-Marshaling
6. **Serialisierung:** `System.Text.Json` mit entsprechenden `JsonSerializerOptions` für formatierte Ausgabe verwenden
7. **Dateidialoge:** `OpenFileDialog`, `SaveFileDialog` und `FolderBrowserDialog` mit entsprechenden Filtern verwenden

---

## Projektstruktur

```
REFATimeStudy/
├── REFATimeStudy.sln
├── REFATimeStudy/
│   ├── Program.cs
│   ├── MainForm.cs / .Designer.cs / .resx
│   ├── DefinitionEditorControl.cs / .Designer.cs
│   ├── RecordingControl.cs / .Designer.cs
│   ├── ResultsControl.cs / .Designer.cs
│   ├── SettingsDialog.cs / .Designer.cs / .resx
│   ├── AboutDialog.cs / .Designer.cs / .resx
│   ├── Models/
│   │   ├── TimeStudyDefinition.cs
│   │   ├── ProcessStepDefinition.cs
│   │   ├── TimeStudyRecording.cs
│   │   ├── TimeEntry.cs
│   │   └── ApplicationSettings.cs
│   ├── Services/
│   │   ├── DefinitionService.cs
│   │   ├── RecordingService.cs
│   │   ├── SettingsService.cs
│   │   └── CsvExportService.cs
│   ├── Properties/
│   │   └── Resources.resx
│   │   └── Resources.de.resx
│   └── REFATimeStudy.csproj
```

---

## Abnahmekriterien

1. Benutzer kann eine neue Zeitaufnahme-Definition mit mehreren Ablaufabschnitten erstellen
2. Benutzer kann einen Ablaufabschnitt als Standard (Auffang) kennzeichnen
3. Benutzer kann Definitionen als JSON speichern und neu laden
4. Gesperrte Definitionen (mit Aufnahmen) können nicht bearbeitet, aber kopiert werden
5. Benutzer kann eine Aufnahme mit touch-freundlichen Schaltflächen durchführen
6. Zeit akkumuliert korrekt auf dem Standard-Ablaufabschnitt, wenn kein spezifischer Abschnitt ausgewählt ist
7. Abgeschlossene Aufnahmen können als CSV mit Detail- und Zusammenfassungsbereichen exportiert werden
8. UI-Sprache kann über Einstellungen zwischen Deutsch und Englisch gewechselt werden
9. Basisverzeichnis für Dateispeicherung kann konfiguriert werden
10. Anwendung behandelt High-DPI-Bildschirme korrekt

---

## Hinweise zur Implementierung

- Der typische Benutzer (wie Gabriele) verwendet oft konsistente Auftragsnummern über Studien hinweg: beispielsweise immer 18 für Sachliche Verteilzeit, 19 für Unterbrechung durch Zeitnehmer (UZA) und 40 für den Standardabschnitt. Das Auftragsnummernfeld sollte Lücken erlauben.

- Ablaufabschnitt-Schaltflächen während der Aufnahme sollten deutlich anzeigen, welcher Abschnitt aktuell aktiv ist (andere Hintergrundfarbe oder Rahmen).

- Die Fortschrittszeit-Anzeige sollte während der Aufnahme in Echtzeit aktualisiert werden (etwa alle 100ms).

- Beim Export in CSV Semikolons als Trennzeichen für deutsche Lokalisierungskompatibilität mit Excel verwenden, oder eine Option zur Auswahl des Trennzeichens bereitstellen.

---

*Dieser Prompt ist für die Verwendung mit GitHub Copilot in Visual Studio konzipiert, um eine vollständige, funktionsfähige WinForms-Anwendung für REFA-Zeitaufnahmen zu generieren.*
