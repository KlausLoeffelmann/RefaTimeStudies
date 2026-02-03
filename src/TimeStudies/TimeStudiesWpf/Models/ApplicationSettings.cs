using System.IO;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Application-wide settings stored in user's AppData folder.
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
    public string BaseDirectory { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "REFATimeStudy");

    /// <summary>
    /// Minimum button size in pixels for process step buttons.
    /// </summary>
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    /// Whether to confirm before closing an active recording.
    /// </summary>
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    /// Whether to auto-save recordings periodically.
    /// </summary>
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    /// CSV delimiter character (semicolon for German Excel compatibility).
    /// </summary>
    public char CsvDelimiter { get; set; } = ';';
}
