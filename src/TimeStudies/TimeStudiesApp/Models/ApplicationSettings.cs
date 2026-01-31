namespace TimeStudiesApp.Models;

/// <summary>
///  Application-wide settings for the REFA Time Study application.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    ///  Gets or sets the UI language code (e.g., "de-DE" or "en-US").
    /// </summary>
    public string UiLanguage { get; set; } = "en-US";

    /// <summary>
    ///  Gets or sets the base directory where definitions and recordings are stored.
    /// </summary>
    public string BaseDirectory { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the minimum button size in pixels for touch-friendly buttons.
    /// </summary>
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    ///  Gets or sets a value indicating whether to confirm before closing a recording.
    /// </summary>
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    ///  Gets or sets a value indicating whether recordings should be auto-saved.
    /// </summary>
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    ///  Gets or sets the CSV delimiter character.
    ///  Default is semicolon for German Excel compatibility.
    /// </summary>
    public string CsvDelimiter { get; set; } = ";";

    /// <summary>
    ///  Gets the definitions directory path.
    /// </summary>
    public string DefinitionsDirectory =>
        Path.Combine(BaseDirectory, "Definitions");

    /// <summary>
    ///  Gets the recordings directory path.
    /// </summary>
    public string RecordingsDirectory =>
        Path.Combine(BaseDirectory, "Recordings");

    /// <summary>
    ///  Gets the default settings file path in AppData.
    /// </summary>
    public static string DefaultSettingsPath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "REFATimeStudy",
            "settings.json");

    /// <summary>
    ///  Creates settings with sensible defaults.
    /// </summary>
    /// <returns>New settings instance with defaults.</returns>
    public static ApplicationSettings CreateDefault()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return new ApplicationSettings
        {
            BaseDirectory = Path.Combine(documentsPath, "REFATimeStudy")
        };
    }
}
