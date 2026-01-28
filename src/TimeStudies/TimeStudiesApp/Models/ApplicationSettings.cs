using System.Drawing;

namespace TimeStudiesApp.Models;

/// <summary>
/// Application settings that persist between sessions.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// UI language code (e.g., "de-DE" or "en-US").
    /// </summary>
    public string UiLanguage { get; set; } = "en-US";

    /// <summary>
    /// Base directory for storing definitions and recordings.
    /// </summary>
    public string BaseDirectory { get; set; } = GetDefaultBaseDirectory();

    /// <summary>
    /// Minimum button size in pixels for touch-friendly UI.
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
    /// CSV delimiter to use for export.
    /// </summary>
    public string CsvDelimiter { get; set; } = ";";

    /// <summary>
    /// Main form window state.
    /// </summary>
    public FormWindowState MainFormWindowState { get; set; } = FormWindowState.Normal;

    /// <summary>
    /// Main form location (when not maximized).
    /// </summary>
    public Point MainFormLocation { get; set; } = new Point(100, 100);

    /// <summary>
    /// Main form size (when not maximized).
    /// </summary>
    public Size MainFormSize { get; set; } = new Size(1024, 768);

    private static string GetDefaultBaseDirectory()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appData, "REFATimeStudy", "Data");
    }

    /// <summary>
    /// Gets the full path to the definitions directory.
    /// </summary>
    public string GetDefinitionsDirectory() =>
        Path.Combine(BaseDirectory, "Definitions");

    /// <summary>
    /// Gets the full path to the recordings directory.
    /// </summary>
    public string GetRecordingsDirectory() =>
        Path.Combine(BaseDirectory, "Recordings");

    /// <summary>
    /// Ensures all required directories exist.
    /// </summary>
    public void EnsureDirectoriesExist()
    {
        Directory.CreateDirectory(GetDefinitionsDirectory());
        Directory.CreateDirectory(GetRecordingsDirectory());
    }
}
