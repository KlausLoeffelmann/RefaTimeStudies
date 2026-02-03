using System.IO;
using System.Text.Json;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Manages application settings stored in the user's AppData folder.
/// </summary>
public class SettingsService : ISettingsService
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "REFATimeStudy");

    private static readonly string SettingsFilePath = Path.Combine(
        SettingsDirectory, "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private ApplicationSettings? _current;

    /// <summary>
    /// Gets the current settings (cached).
    /// </summary>
    public ApplicationSettings Current => _current ??= Load();

    /// <summary>
    /// Loads the application settings from disk.
    /// </summary>
    public ApplicationSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);
                if (settings is not null)
                {
                    _current = settings;
                    return settings;
                }
            }
        }
        catch
        {
            // If loading fails, return defaults
        }

        _current = new ApplicationSettings();
        return _current;
    }

    /// <summary>
    /// Saves the application settings to disk.
    /// </summary>
    public void Save(ApplicationSettings settings)
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);
            var json = JsonSerializer.Serialize(settings, JsonOptions);
            File.WriteAllText(SettingsFilePath, json);
            _current = settings;
        }
        catch
        {
            // Log or handle save errors
        }
    }

    /// <summary>
    /// Ensures the base directories for definitions and recordings exist.
    /// </summary>
    public void EnsureDirectoriesExist()
    {
        var settings = Current;

        var definitionsDir = Path.Combine(settings.BaseDirectory, "Definitions");
        var recordingsDir = Path.Combine(settings.BaseDirectory, "Recordings");

        Directory.CreateDirectory(definitionsDir);
        Directory.CreateDirectory(recordingsDir);
    }
}
