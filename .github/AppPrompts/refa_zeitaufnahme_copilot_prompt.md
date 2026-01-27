# Copilot Prompt: REFA Time Study Application (Zeitaufnahme-Anwendung)

## Overview

Create a Windows Forms application in C# (.NET 9 or higher) for conducting REFA-style industrial time studies (*Zeitaufnahmen*). The application is designed for use on touch-enabled devices like the Microsoft Surface Go 3.

The application must support **full German/English localization** using standard WinForms resource files (.resx), with terminology strictly following REFA standards (see terminology table below).

---

## Core Concepts and Terminology

The application deals with these REFA concepts. Use exactly these terms in the respective language:

| Concept | German Term (UI) | English Term (UI) | Description |
|---------|------------------|-------------------|-------------|
| Time Study Definition | Zeitaufnahme-Definition | Time Study Definition | Template defining what process steps to measure |
| Time Study | Zeitaufnahme | Time Study | An actual recording session using a definition |
| Process Step | Ablaufabschnitt | Process Step | A single measurable operation element |
| Default Process Step | Standard-Ablaufabschnitt | Default Process Step | Catches time not assigned to specific steps |
| Actual Time | Ist-Zeit | Actual Time | The measured/observed time |
| Progressive Time | Fortschrittszeit | Progressive Time | Cumulative time from study start |
| Analyst Interruption | Unterbrechung durch Zeitnehmer (UZA) | Analyst Interruption | Pause caused by the analyst |
| Personal Allowance | Persönliche Verteilzeit | Personal Allowance | Time for personal needs |
| Process Allowance | Sachliche Verteilzeit | Process Allowance | Time for process-related interruptions |
| Order Number | Auftragsnummer | Order Number | Reference number for a process step |
| Start Recording | Aufnahme starten | Start Recording | Begin the time study |
| Stop Recording | Aufnahme stoppen | Stop Recording | End the time study |
| Pause | Unterbrechen | Pause | Temporarily pause to default step |
| Piece/Unit | Stück | Piece | The item being produced/processed |

---

## Application Structure

### 1. Main Window

The main window should have:

- A **menu bar** with:
  - File: New Definition, Open Definition, Save Definition, Save Definition As, Export Time Study (CSV), Exit
  - Tools: Settings/Options
  - Help: About

- A **toolbar** with large, touch-friendly buttons (minimum 48x48 pixels) for:
  - New Definition
  - Open Definition
  - Save Definition
  - Start/Stop Recording
  - Export CSV

- A **main content area** that switches between:
  - Definition Editor (when creating/editing definitions)
  - Recording View (when conducting a time study)
  - Results View (when reviewing completed studies)

---

### 2. Time Study Definition (Zeitaufnahme-Definition)

A definition consists of:

```csharp
public class TimeStudyDefinition
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsLocked { get; set; } // True if recordings exist
    public List<ProcessStepDefinition> ProcessSteps { get; set; }
    public int DefaultProcessStepOrderNumber { get; set; } // Which step is the default
}

public class ProcessStepDefinition
{
    public int OrderNumber { get; set; } // User-defined, e.g., 1, 2, 3... or 18, 19, 20
    public string Description { get; set; }
    public string ProductDescription { get; set; } // What is being processed
    public DimensionType DimensionType { get; set; } // Weight, Count, Length, etc.
    public string DimensionUnit { get; set; } // kg, pieces, meters, etc.
    public decimal DefaultDimensionValue { get; set; }
    public bool IsDefaultStep { get; set; } // Marks the "catch-all" step
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

**Definition Editor Features:**

- DataGridView or similar for editing process steps
- Ability to add, remove, reorder steps
- Designation of exactly one step as the Default Process Step (*Standard-Ablaufabschnitt*)
- Order numbers should be user-definable (not auto-increment) because analysts often use consistent numbering schemes (e.g., 18 = Process Allowance, 19 = Analyst Interruption, 20 = Personal Allowance)
- Gap in numbering is allowed (1, 2, 5, 18, 19 is valid)

**Locking Behavior:**

- Once a time study has been recorded against a definition, that definition becomes **locked** (`IsLocked = true`)
- Locked definitions cannot be edited
- Provide option to "Create Copy" (*Kopie erstellen* / *Create Copy*) of a locked definition for modification

---

### 3. Time Study Recording (Zeitaufnahme)

The recording view should be optimized for touch operation:

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
    public int SequenceNumber { get; set; } // Auto-incrementing
    public int ProcessStepOrderNumber { get; set; }
    public string ProcessStepDescription { get; set; }
    public DateTime Timestamp { get; set; }
    public TimeSpan ElapsedFromStart { get; set; } // Fortschrittszeit
    public TimeSpan Duration { get; set; } // Calculated when next entry is made
    public decimal? DimensionValue { get; set; } // Override for this entry
}
```

**Recording View UI:**

- Large **Start Recording** (*Aufnahme starten*) button at top
- Large **Stop Recording** (*Aufnahme stoppen*) button (disabled until started)
- **Pause/Interrupt** (*Unterbrechen*) button that switches to the Default Process Step
- **Process Step Buttons**: A grid of large, touch-friendly buttons (minimum 60x60 pixels), one for each defined process step, showing:
  - Order Number prominently
  - Short description
  - Visual feedback when currently active
- **Real-time display** showing:
  - Current progressive time (*Fortschrittszeit*)
  - Current active process step
  - Entry count

**Recording Logic:**

1. User clicks "Start Recording" - clock starts, time flows to Default Process Step
2. User clicks a Process Step button - records timestamp, calculates duration of previous step
3. When clicking "Pause/Interrupt" - switches to Default Process Step (same as clicking that step's button)
4. User clicks "Stop Recording" - final entry made, recording marked complete

**Important:** Time always flows somewhere. If the analyst doesn't click a specific step, time accumulates on the Default Process Step. This matches the described workflow where undefined periods show as "empty" times on the default step.

---

### 4. CSV Export

Export completed time studies with this structure:

```csv
"Definition","Recording ID","Started At","Completed At"
"Mangel Großteile","abc-123","2025-11-29 14:30:00","2025-11-29 15:45:00"

"Seq","Order#","Description","Timestamp","Progressive Time","Duration (s)","Duration (min)","Dimension Value"
1,40,"Unterbrechung durch Zeitnehmer","2025-11-29 14:30:00","00:00:00",0,0.00,
2,1,"Container holen","2025-11-29 14:30:15","00:00:15",15,0.25,1
3,2,"Wäsche einklammern","2025-11-29 14:30:45","00:00:45",30,0.50,12
...
```

Also provide a **summary section** in the CSV:

```csv
"SUMMARY"
"Order#","Description","Count","Total Duration (s)","Total Duration (min)","Avg Duration (s)"
1,"Container holen",5,75,1.25,15.0
2,"Wäsche einklammern",42,1260,21.0,30.0
...
```

---

### 5. Settings Dialog (Tools > Options)

Settings should include:

```csharp
public class ApplicationSettings
{
    public string UiLanguage { get; set; } // "de-DE" or "en-US"
    public string BaseDirectory { get; set; } // Where to store definitions and recordings
    public int ButtonSize { get; set; } // Minimum button size in pixels (default 60)
    public bool ConfirmBeforeClosingRecording { get; set; }
    public bool AutoSaveRecordings { get; set; }
}
```

**Settings UI:**

- **Language** (*Sprache* / *Language*): Dropdown with "Deutsch" and "English"
  - Language change should prompt for application restart
- **Base Directory** (*Basisverzeichnis* / *Base Directory*): Folder browser for where JSON definitions and recordings are stored
- **Button Size** (*Schaltflächengröße* / *Button Size*): Slider or numeric input
- **Auto-save** (*Automatisch speichern* / *Auto-save*): Checkbox

Store settings in `%AppData%\REFATimeStudy\settings.json`

---

### 6. File Storage

Use JSON format for both definitions and recordings:

**Definition files:** `{BaseDirectory}\Definitions\{Name}_{Id}.json`
**Recording files:** `{BaseDirectory}\Recordings\{DefinitionName}_{RecordingId}_{Date}.json`

---

## Localization Requirements

Create resource files:
- `Resources.resx` (default/English)
- `Resources.de.resx` (German)

All UI strings must come from resources. Key strings to include:

| Resource Key | English | German |
|-------------|---------|--------|
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

## Technical Requirements

1. **Target Framework:** .NET 9.0 Windows (or .NET 8.0 LTS)
2. **Touch-friendly:** All interactive elements minimum 48x48 pixels, process step buttons 60x60 or larger
3. **High DPI aware:** Use proper DPI scaling
4. **Responsive timing:** Use `System.Diagnostics.Stopwatch` for accurate timing, not DateTime
5. **Thread safety:** Recording timer updates must be thread-safe with proper UI thread marshaling
6. **Serialization:** Use `System.Text.Json` with proper `JsonSerializerOptions` for pretty-printing
7. **File dialogs:** Use `OpenFileDialog`, `SaveFileDialog`, and `FolderBrowserDialog` with appropriate filters

---

## Project Structure

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

## Acceptance Criteria

1. User can create a new time study definition with multiple process steps
2. User can designate one process step as the default (catch-all) step
3. User can save definitions as JSON and reload them
4. Locked definitions (with recordings) cannot be edited but can be copied
5. User can conduct a recording with touch-friendly buttons
6. Time accurately accumulates on the default step when no specific step is selected
7. Completed recordings can be exported to CSV with both detail and summary sections
8. UI language can be switched between German and English via settings
9. Base directory for file storage can be configured
10. Application handles high-DPI displays correctly

---

## Notes for Implementation

- The typical user (like Gabriele) often uses consistent order numbers across studies: for example, always using 18 for Process Allowance (*Sachliche Verteilzeit*), 19 for Analyst Interruption (*UZA*), and 40 for the default step. The order number field should allow gaps.

- Process step buttons during recording should clearly indicate which step is currently active (different background color or border).

- The progressive time display should update in real-time (approximately every 100ms) during recording.

- When exporting to CSV, use semicolons as separators for German locale compatibility with Excel, or provide an option to choose the delimiter.

---

*This prompt is designed for use with GitHub Copilot in Visual Studio to generate a complete, functional WinForms application for REFA time studies.*
