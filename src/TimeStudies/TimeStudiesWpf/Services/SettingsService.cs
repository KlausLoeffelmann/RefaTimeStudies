using System.IO;
using TimeStudiesWpf.Helpers;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Service for managing application settings.
/// Dienst für die Verwaltung von Anwendungseinstellungen.
/// </summary>
public class SettingsService
{
    private readonly string _settingsFilePath;
    private ApplicationSettings? _settings;

    /// <summary>
    /// Gets the current application settings.
    /// Ruft die aktuellen Anwendungseinstellungen ab.
    /// </summary>
    public ApplicationSettings Settings
    {
        get
        {
            if (_settings == null)
            {
                LoadSettings();
            }

            return _settings!;
        }
    }

    /// <summary>
    /// Initializes a new instance of SettingsService.
    /// Initialisiert eine neue Instanz von SettingsService.
    /// </summary>
    public SettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var settingsDir = Path.Combine(appDataPath, "REFATimeStudy");

        if (!Directory.Exists(settingsDir))
        {
            Directory.CreateDirectory(settingsDir);
        }

        _settingsFilePath = Path.Combine(settingsDir, "settings.json");
    }

    /// <summary>
    /// Loads the application settings from file.
    /// Lädt die Anwendungseinstellungen aus einer Datei.
    /// </summary>
    /// <returns>The loaded settings, or default settings if file doesn't exist.</returns>
    public ApplicationSettings LoadSettings()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                _settings = JsonSerializerHelper.DeserializeFromFile<ApplicationSettings>(_settingsFilePath);
                if (_settings != null)
                {
                    _settings.InitializeDefaults();
                    return _settings;
                }
            }
        }
        catch
        {
            // If loading fails, fall back to default settings
        }

        _settings = new ApplicationSettings();
        _settings.InitializeDefaults();
        SaveSettings();

        return _settings;
    }

    /// <summary>
    /// Saves the current application settings to file.
    /// Speichert die aktuellen Anwendungseinstellungen in einer Datei.
    /// </summary>
    public void SaveSettings()
    {
        if (_settings != null)
        {
            JsonSerializerHelper.SerializeToFile(_settings, _settingsFilePath);
        }
    }

    /// <summary>
    /// Gets the file path where settings are stored.
    /// Ruft den Dateipfad ab, in dem die Einstellungen gespeichert sind.
    /// </summary>
    /// <returns>The settings file path.</returns>
    public string GetSettingsPath()
    {
        return _settingsFilePath;
    }

    /// <summary>
    /// Resets settings to default values.
    /// Setzt die Einstellungen auf Standardwerte zurück.
    /// </summary>
    public void ResetSettings()
    {
        _settings = new ApplicationSettings();
        _settings.InitializeDefaults();
        SaveSettings();
    }

    /// <summary>
    /// Updates the UI language setting.
    /// Aktualisiert die Spracheinstellung der Benutzeroberfläche.
    /// </summary>
    /// <param name="language">The language code (e.g., "de-DE" or "en-US").</param>
    public void SetLanguage(string language)
    {
        if (_settings != null)
        {
            _settings.UiLanguage = language;
            SaveSettings();
        }
    }

    /// <summary>
    /// Updates the base directory setting.
    /// Aktualisiert die Einstellung für das Basisverzeichnis.
    /// </summary>
    /// <param name="directory">The base directory path.</param>
    public void SetBaseDirectory(string directory)
    {
        if (_settings != null)
        {
            _settings.BaseDirectory = directory;
            SaveSettings();
        }
    }

    /// <summary>
    /// Updates the button size setting.
    /// Aktualisiert die Einstellung für die Schaltflächengröße.
    /// </summary>
    /// <param name="size">The minimum button size in pixels.</param>
    public void SetButtonSize(int size)
    {
        if (_settings != null)
        {
            _settings.ButtonSize = Math.Max(48, size);
            SaveSettings();
        }
    }

    /// <summary>
    /// Updates the auto-save setting.
    /// Aktualisiert die Einstellung für das automatische Speichern.
    /// </summary>
    /// <param name="autoSave">Whether to auto-save recordings.</param>
    public void SetAutoSave(bool autoSave)
    {
        if (_settings != null)
        {
            _settings.AutoSaveRecordings = autoSave;
            SaveSettings();
        }
    }

    /// <summary>
    /// Updates the confirm before closing recording setting.
    /// Aktualisiert die Einstellung für die Bestätigung vor dem Schließen einer Aufnahme.
    /// </summary>
    /// <param name="confirm">Whether to confirm before closing.</param>
    public void SetConfirmBeforeClosing(bool confirm)
    {
        if (_settings != null)
        {
            _settings.ConfirmBeforeClosingRecording = confirm;
            SaveSettings();
        }
    }
}