using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for loading and saving application settings.
/// </summary>
public sealed class SettingsService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static readonly Lazy<SettingsService> LazyInstance = new(() => new SettingsService());

    private ApplicationSettings? _currentSettings;

    private SettingsService() { }

    /// <summary>
    /// Gets the singleton instance of the settings service.
    /// </summary>
    public static SettingsService Instance => LazyInstance.Value;

    /// <summary>
    /// Gets the path to the settings file.
    /// </summary>
    private static string SettingsFilePath
    {
        get
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string settingsDir = Path.Combine(appData, "REFATimeStudy");
            Directory.CreateDirectory(settingsDir);
            return Path.Combine(settingsDir, "settings.json");
        }
    }

    /// <summary>
    /// Gets the current settings, loading from disk if necessary.
    /// </summary>
    public ApplicationSettings Settings => _currentSettings ??= LoadSettings();

    /// <summary>
    /// Loads settings from the configuration file.
    /// </summary>
    public ApplicationSettings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                ApplicationSettings? settings = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);

                if (settings is not null)
                {
                    _currentSettings = settings;
                    return settings;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
        }

        // Return default settings if file doesn't exist or couldn't be loaded
        _currentSettings = new ApplicationSettings();
        return _currentSettings;
    }

    /// <summary>
    /// Saves the current settings to the configuration file.
    /// </summary>
    public void SaveSettings()
    {
        ArgumentNullException.ThrowIfNull(_currentSettings);
        SaveSettings(_currentSettings);
    }

    /// <summary>
    /// Saves the specified settings to the configuration file.
    /// </summary>
    public void SaveSettings(ApplicationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        try
        {
            string json = JsonSerializer.Serialize(settings, JsonOptions);
            File.WriteAllText(SettingsFilePath, json);
            _currentSettings = settings;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Resets settings to defaults.
    /// </summary>
    public ApplicationSettings ResetToDefaults()
    {
        _currentSettings = new ApplicationSettings();
        SaveSettings(_currentSettings);
        return _currentSettings;
    }

    /// <summary>
    /// Updates the main form position in settings.
    /// </summary>
    public void UpdateMainFormPosition(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        if (form.WindowState == FormWindowState.Normal)
        {
            Settings.MainFormLocation = form.Location;
            Settings.MainFormSize = form.Size;
        }

        Settings.MainFormWindowState = form.WindowState;
    }

    /// <summary>
    /// Applies the saved main form position to the form.
    /// </summary>
    public void ApplyMainFormPosition(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        // Ensure the form is visible on at least one screen
        Rectangle formBounds = new(Settings.MainFormLocation, Settings.MainFormSize);
        bool isVisible = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(formBounds));

        if (isVisible)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = Settings.MainFormLocation;
            form.Size = Settings.MainFormSize;
        }
        else
        {
            // Center on primary screen if saved position is not visible
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = Settings.MainFormSize;
        }

        // Apply window state after position is set
        form.WindowState = Settings.MainFormWindowState;
    }
}
