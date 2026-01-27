using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for managing application settings.
/// Settings are stored in %AppData%\REFATimeStudy\settings.json
/// </summary>
public class SettingsService
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "REFATimeStudy");

    private static readonly string SettingsFilePath = Path.Combine(SettingsDirectory, "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private ApplicationSettings? _currentSettings;

    /// <summary>
    /// Gets the current application settings, loading from disk if not already loaded.
    /// </summary>
    public ApplicationSettings CurrentSettings
    {
        get
        {
            _currentSettings ??= Load();
            return _currentSettings;
        }
    }

    /// <summary>
    /// Loads settings from disk, or returns defaults if file doesn't exist.
    /// </summary>
    public ApplicationSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);
                if (settings != null)
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

        // Return default settings
        _currentSettings = new ApplicationSettings();
        return _currentSettings;
    }

    /// <summary>
    /// Saves the current settings to disk.
    /// </summary>
    public void Save()
    {
        Save(CurrentSettings);
    }

    /// <summary>
    /// Saves the specified settings to disk.
    /// </summary>
    public void Save(ApplicationSettings settings)
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);
            
            var json = JsonSerializer.Serialize(settings, JsonOptions);
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
    /// Updates the main form position in settings.
    /// </summary>
    public void UpdateMainFormPosition(Form form)
    {
        if (form.WindowState == FormWindowState.Normal)
        {
            CurrentSettings.MainFormX = form.Left;
            CurrentSettings.MainFormY = form.Top;
            CurrentSettings.MainFormWidth = form.Width;
            CurrentSettings.MainFormHeight = form.Height;
        }
        CurrentSettings.MainFormWindowState = form.WindowState;
    }

    /// <summary>
    /// Restores the main form position from settings.
    /// </summary>
    public void RestoreMainFormPosition(Form form)
    {
        var settings = CurrentSettings;

        // Only restore if we have valid saved values
        if (settings.MainFormX.HasValue && settings.MainFormY.HasValue &&
            settings.MainFormWidth.HasValue && settings.MainFormHeight.HasValue)
        {
            // Verify the position is on a visible screen
            var savedBounds = new Rectangle(
                settings.MainFormX.Value,
                settings.MainFormY.Value,
                settings.MainFormWidth.Value,
                settings.MainFormHeight.Value);

            bool isVisible = Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(savedBounds));

            if (isVisible)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Left = settings.MainFormX.Value;
                form.Top = settings.MainFormY.Value;
                form.Width = settings.MainFormWidth.Value;
                form.Height = settings.MainFormHeight.Value;
            }
        }

        // Restore window state (but not minimized)
        if (settings.MainFormWindowState == FormWindowState.Maximized)
        {
            form.WindowState = FormWindowState.Maximized;
        }
    }

    /// <summary>
    /// Applies the current language setting to the application.
    /// </summary>
    public void ApplyLanguage()
    {
        var culture = new System.Globalization.CultureInfo(CurrentSettings.UiLanguage);
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
    }

    /// <summary>
    /// Checks if the language has changed from the specified original value.
    /// </summary>
    public bool HasLanguageChanged(string originalLanguage)
    {
        return !string.Equals(CurrentSettings.UiLanguage, originalLanguage, StringComparison.OrdinalIgnoreCase);
    }
}
