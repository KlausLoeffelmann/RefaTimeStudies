using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing application settings.
/// </summary>
public class SettingsService
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "REFATimeStudy");

    private static readonly string SettingsFilePath = Path.Combine(
        SettingsDirectory,
        "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Loads application settings from the settings file.
    /// Returns default settings if the file doesn't exist.
    /// </summary>
    public ApplicationSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);
                return settings ?? new ApplicationSettings();
            }
        }
        catch (Exception)
        {
            // If loading fails, return defaults
        }

        return new ApplicationSettings();
    }

    /// <summary>
    /// Saves application settings to the settings file.
    /// </summary>
    public void Save(ApplicationSettings settings)
    {
        EnsureSettingsDirectoryExists();

        string json = JsonSerializer.Serialize(settings, JsonOptions);
        File.WriteAllText(SettingsFilePath, json);
    }

    /// <summary>
    /// Ensures the settings directory exists.
    /// </summary>
    public void EnsureSettingsDirectoryExists()
    {
        Directory.CreateDirectory(SettingsDirectory);
    }

    /// <summary>
    /// Gets the path to the settings file.
    /// </summary>
    public static string GetSettingsFilePath() => SettingsFilePath;
}
