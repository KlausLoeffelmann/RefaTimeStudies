using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
///  Service for managing application settings.
/// </summary>
public class SettingsService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private ApplicationSettings _settings;
    private readonly string _settingsPath;

    /// <summary>
    ///  Gets the current application settings.
    /// </summary>
    public ApplicationSettings Settings => _settings;

    /// <summary>
    ///  Initializes a new instance of the <see cref="SettingsService"/> class.
    /// </summary>
    /// <param name="settingsPath">Optional custom path for settings file.</param>
    public SettingsService(string? settingsPath = null)
    {
        _settingsPath = settingsPath ?? ApplicationSettings.DefaultSettingsPath;
        _settings = ApplicationSettings.CreateDefault();
    }

    /// <summary>
    ///  Loads settings from the settings file.
    /// </summary>
    /// <returns>True if settings were loaded successfully.</returns>
    public bool Load()
    {
        try
        {
            if (!File.Exists(_settingsPath))
            {
                _settings = ApplicationSettings.CreateDefault();
                EnsureDirectoriesExist();
                return false;
            }

            string json = File.ReadAllText(_settingsPath);
            var loaded = JsonSerializer.Deserialize<ApplicationSettings>(json, s_jsonOptions);

            if (loaded is not null)
            {
                _settings = loaded;
                EnsureDirectoriesExist();
                return true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
        }

        _settings = ApplicationSettings.CreateDefault();
        EnsureDirectoriesExist();
        return false;
    }

    /// <summary>
    ///  Saves the current settings to the settings file.
    /// </summary>
    /// <returns>True if settings were saved successfully.</returns>
    public bool Save()
    {
        try
        {
            string? directory = Path.GetDirectoryName(_settingsPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(_settings, s_jsonOptions);
            File.WriteAllText(_settingsPath, json);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    ///  Updates the UI language setting.
    /// </summary>
    /// <param name="languageCode">The language code (e.g., "de-DE" or "en-US").</param>
    public void SetLanguage(string languageCode)
    {
        _settings.UiLanguage = languageCode;
    }

    /// <summary>
    ///  Updates the base directory setting.
    /// </summary>
    /// <param name="path">The new base directory path.</param>
    public void SetBaseDirectory(string path)
    {
        _settings.BaseDirectory = path;
        EnsureDirectoriesExist();
    }

    /// <summary>
    ///  Ensures all required directories exist.
    /// </summary>
    private void EnsureDirectoriesExist()
    {
        try
        {
            if (!string.IsNullOrEmpty(_settings.BaseDirectory))
            {
                if (!Directory.Exists(_settings.DefinitionsDirectory))
                {
                    Directory.CreateDirectory(_settings.DefinitionsDirectory);
                }

                if (!Directory.Exists(_settings.RecordingsDirectory))
                {
                    Directory.CreateDirectory(_settings.RecordingsDirectory);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error creating directories: {ex.Message}");
        }
    }
}
