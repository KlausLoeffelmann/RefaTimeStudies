namespace TimeStudiesApp.Models;

/// <summary>
/// Application settings for the REFA Time Study application.
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
    public string BaseDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Minimum button size in pixels for touch-friendly UI.
    /// </summary>
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    /// If true, prompt user before closing an active recording.
    /// </summary>
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    /// If true, automatically save recordings after each step.
    /// </summary>
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    /// Gets the default base directory path.
    /// </summary>
    public static string GetDefaultBaseDirectory()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "REFATimeStudy");
    }

    /// <summary>
    /// Gets the path for storing definitions.
    /// </summary>
    public string DefinitionsDirectory => Path.Combine(
        string.IsNullOrEmpty(BaseDirectory) ? GetDefaultBaseDirectory() : BaseDirectory,
        "Definitions");

    /// <summary>
    /// Gets the path for storing recordings.
    /// </summary>
    public string RecordingsDirectory => Path.Combine(
        string.IsNullOrEmpty(BaseDirectory) ? GetDefaultBaseDirectory() : BaseDirectory,
        "Recordings");

    /// <summary>
    /// Ensures that required directories exist.
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
            AutoSaveRecordings = AutoSaveRecordings
        };
    }
}
