using System.Drawing;

namespace TimeStudiesApp.Models;

/// <summary>
/// Application settings that are persisted between sessions.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// UI language code ("de-DE" or "en-US").
    /// </summary>
    public string UiLanguage { get; set; } = "en-US";

    /// <summary>
    /// Base directory for storing definitions and recordings.
    /// </summary>
    public string BaseDirectory { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "REFATimeStudy");

    /// <summary>
    /// Minimum button size in pixels for touch-friendly UI (default 60).
    /// </summary>
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    /// Whether to confirm before closing an active recording.
    /// </summary>
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    /// Whether to automatically save recordings.
    /// </summary>
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    /// CSV delimiter character (semicolon for German Excel compatibility).
    /// </summary>
    public string CsvDelimiter { get; set; } = ";";

    /// <summary>
    /// Main form window position X coordinate.
    /// </summary>
    public int? MainFormX { get; set; }

    /// <summary>
    /// Main form window position Y coordinate.
    /// </summary>
    public int? MainFormY { get; set; }

    /// <summary>
    /// Main form window width.
    /// </summary>
    public int? MainFormWidth { get; set; }

    /// <summary>
    /// Main form window height.
    /// </summary>
    public int? MainFormHeight { get; set; }

    /// <summary>
    /// Main form window state (Normal, Maximized, Minimized).
    /// </summary>
    public FormWindowState MainFormWindowState { get; set; } = FormWindowState.Normal;

    /// <summary>
    /// Gets the full path to the Definitions folder.
    /// </summary>
    public string DefinitionsDirectory => Path.Combine(BaseDirectory, "Definitions");

    /// <summary>
    /// Gets the full path to the Recordings folder.
    /// </summary>
    public string RecordingsDirectory => Path.Combine(BaseDirectory, "Recordings");

    /// <summary>
    /// Ensures all required directories exist.
    /// </summary>
    public void EnsureDirectoriesExist()
    {
        Directory.CreateDirectory(DefinitionsDirectory);
        Directory.CreateDirectory(RecordingsDirectory);
    }

    /// <summary>
    /// Creates a copy of these settings.
    /// </summary>
    public ApplicationSettings Clone()
    {
        return new ApplicationSettings
        {
            UiLanguage = UiLanguage,
            BaseDirectory = BaseDirectory,
            ButtonSize = ButtonSize,
            ConfirmBeforeClosingRecording = ConfirmBeforeClosingRecording,
            AutoSaveRecordings = AutoSaveRecordings,
            CsvDelimiter = CsvDelimiter,
            MainFormX = MainFormX,
            MainFormY = MainFormY,
            MainFormWidth = MainFormWidth,
            MainFormHeight = MainFormHeight,
            MainFormWindowState = MainFormWindowState
        };
    }
}
