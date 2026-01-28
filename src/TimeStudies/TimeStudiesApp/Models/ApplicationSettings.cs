using System.Drawing;
using System.Text.Json.Serialization;

namespace TimeStudiesApp.Models;

/// <summary>
/// Application settings persisted to JSON.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// UI language code ("de-DE" or "en-US").
    /// </summary>
    [JsonPropertyName("uiLanguage")]
    public string UiLanguage { get; set; } = "en-US";

    /// <summary>
    /// Base directory for storing definitions and recordings.
    /// </summary>
    [JsonPropertyName("baseDirectory")]
    public string BaseDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Minimum button size in pixels for touch-friendly UI.
    /// </summary>
    [JsonPropertyName("buttonSize")]
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    /// Whether to confirm before closing an active recording.
    /// </summary>
    [JsonPropertyName("confirmBeforeClosingRecording")]
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    /// Whether to auto-save recordings.
    /// </summary>
    [JsonPropertyName("autoSaveRecordings")]
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    /// CSV delimiter character (semicolon for German Excel compatibility).
    /// </summary>
    [JsonPropertyName("csvDelimiter")]
    public string CsvDelimiter { get; set; } = ";";

    /// <summary>
    /// Main window position (X coordinate).
    /// </summary>
    [JsonPropertyName("mainWindowX")]
    public int MainWindowX { get; set; } = -1;

    /// <summary>
    /// Main window position (Y coordinate).
    /// </summary>
    [JsonPropertyName("mainWindowY")]
    public int MainWindowY { get; set; } = -1;

    /// <summary>
    /// Main window width.
    /// </summary>
    [JsonPropertyName("mainWindowWidth")]
    public int MainWindowWidth { get; set; } = 1024;

    /// <summary>
    /// Main window height.
    /// </summary>
    [JsonPropertyName("mainWindowHeight")]
    public int MainWindowHeight { get; set; } = 768;

    /// <summary>
    /// Main window state (Normal, Minimized, Maximized).
    /// </summary>
    [JsonPropertyName("mainWindowState")]
    public FormWindowState MainWindowState { get; set; } = FormWindowState.Normal;

    /// <summary>
    /// Gets the default base directory in user's Documents folder.
    /// </summary>
    public static string GetDefaultBaseDirectory()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "REFA Time Studies");
    }

    /// <summary>
    /// Gets the settings file path.
    /// </summary>
    public static string GetSettingsFilePath()
    {
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "REFATimeStudy");

        return Path.Combine(appDataPath, "settings.json");
    }

    /// <summary>
    /// Ensures the base directory exists.
    /// </summary>
    public void EnsureDirectoriesExist()
    {
        if (string.IsNullOrWhiteSpace(BaseDirectory))
        {
            BaseDirectory = GetDefaultBaseDirectory();
        }

        Directory.CreateDirectory(BaseDirectory);
        Directory.CreateDirectory(Path.Combine(BaseDirectory, "Definitions"));
        Directory.CreateDirectory(Path.Combine(BaseDirectory, "Recordings"));
    }

    /// <summary>
    /// Gets the definitions directory path.
    /// </summary>
    [JsonIgnore]
    public string DefinitionsDirectory => Path.Combine(BaseDirectory, "Definitions");

    /// <summary>
    /// Gets the recordings directory path.
    /// </summary>
    [JsonIgnore]
    public string RecordingsDirectory => Path.Combine(BaseDirectory, "Recordings");

    /// <summary>
    /// Validates window bounds are within visible screen area.
    /// </summary>
    public void ValidateWindowBounds()
    {
        if (MainWindowX < 0 || MainWindowY < 0)
        {
            return; // Will use default centering
        }

        Rectangle windowRect = new(MainWindowX, MainWindowY, MainWindowWidth, MainWindowHeight);
        bool isVisible = false;

        foreach (Screen screen in Screen.AllScreens)
        {
            if (screen.WorkingArea.IntersectsWith(windowRect))
            {
                isVisible = true;
                break;
            }
        }

        if (!isVisible)
        {
            // Reset to defaults if window would be off-screen
            MainWindowX = -1;
            MainWindowY = -1;
        }
    }
}
