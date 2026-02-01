using System.IO;

namespace TimeStudiesWpf.Models;

/// <summary>
/// Application settings for the REFA Time Study application.
/// Anwendungseinstellungen für die REFA-Zeitaufnahme-Anwendung.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// UI language setting ("de-DE" for German, "en-US" for English).
    /// UI-Spracheinstellung ("de-DE" für Deutsch, "en-US" für Englisch).
    /// </summary>
    public string UiLanguage { get; set; } = "en-US";

    /// <summary>
    /// Base directory where definitions and recordings are stored.
    /// Basisverzeichnis, in dem Definitionen und Aufnahmen gespeichert werden.
    /// </summary>
    public string BaseDirectory { get; set; } = string.Empty;

    /// <summary>
    /// Minimum button size in pixels for touch-friendly UI.
    /// Minimale Schaltflächengröße in Pixeln für touch-freundliche Benutzeroberfläche.
    /// </summary>
    public int ButtonSize { get; set; } = 60;

    /// <summary>
    /// Indicates whether to prompt for confirmation before closing a recording.
    /// Gibt an, ob vor dem Schließen einer Aufnahme eine Bestätigung angefordert werden soll.
    /// </summary>
    public bool ConfirmBeforeClosingRecording { get; set; } = true;

    /// <summary>
    /// Indicates whether to automatically save recordings during the session.
    /// Gibt an, ob Aufnahmen während der Sitzung automatisch gespeichert werden sollen.
    /// </summary>
    public bool AutoSaveRecordings { get; set; } = true;

    /// <summary>
    /// Gets the default base directory path if none is set.
    /// Gibt den Standard-Basisverzeichnispfad zurück, wenn keiner festgelegt ist.
    /// </summary>
    /// <returns>The default base directory path.</returns>
    public static string GetDefaultBaseDirectory()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(appDataPath, "REFATimeStudy");
    }

    /// <summary>
    /// Initializes default values for the settings.
    /// Initialisiert Standardwerte für die Einstellungen.
    /// </summary>
    public void InitializeDefaults()
    {
        if (string.IsNullOrEmpty(BaseDirectory))
        {
            BaseDirectory = GetDefaultBaseDirectory();
        }

        if (string.IsNullOrEmpty(UiLanguage))
        {
            UiLanguage = "en-US";
        }

        if (ButtonSize < 48)
        {
            ButtonSize = 60;
        }
    }
}