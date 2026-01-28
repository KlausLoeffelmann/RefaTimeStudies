using System.Text.Json;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for loading and saving application settings.
/// </summary>
public class SettingsService
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true
    };

    private ApplicationSettings? _settings;

    /// <summary>
    /// Gets the current application settings.
    /// </summary>
    public ApplicationSettings Settings => _settings ??= LoadSettings();

    /// <summary>
    /// Loads settings from the settings file.
    /// </summary>
    public ApplicationSettings LoadSettings()
    {
        string settingsPath = ApplicationSettings.GetSettingsFilePath();

        if (File.Exists(settingsPath))
        {
            try
            {
                string json = File.ReadAllText(settingsPath);
                ApplicationSettings? settings = JsonSerializer.Deserialize<ApplicationSettings>(json, s_jsonOptions);

                if (settings is not null)
                {
                    settings.ValidateWindowBounds();
                    _settings = settings;
                    return settings;
                }
            }
            catch (JsonException)
            {
                // Invalid JSON, return defaults
            }
            catch (IOException)
            {
                // File access error, return defaults
            }
        }

        // Return defaults
        _settings = new ApplicationSettings
        {
            BaseDirectory = ApplicationSettings.GetDefaultBaseDirectory()
        };

        return _settings;
    }

    /// <summary>
    /// Saves settings to the settings file.
    /// </summary>
    public void SaveSettings()
    {
        SaveSettings(Settings);
    }

    /// <summary>
    /// Saves the specified settings to the settings file.
    /// </summary>
    /// <param name="settings">The settings to save.</param>
    public void SaveSettings(ApplicationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        string settingsPath = ApplicationSettings.GetSettingsFilePath();
        string? directory = Path.GetDirectoryName(settingsPath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(settings, s_jsonOptions);
        File.WriteAllText(settingsPath, json);

        _settings = settings;
    }

    /// <summary>
    /// Updates the main window bounds in settings.
    /// </summary>
    /// <param name="form">The main form to capture bounds from.</param>
    public void UpdateWindowBounds(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        if (form.WindowState == FormWindowState.Normal)
        {
            Settings.MainWindowX = form.Location.X;
            Settings.MainWindowY = form.Location.Y;
            Settings.MainWindowWidth = form.Size.Width;
            Settings.MainWindowHeight = form.Size.Height;
        }

        Settings.MainWindowState = form.WindowState;
    }

    /// <summary>
    /// Applies stored window bounds to a form.
    /// </summary>
    /// <param name="form">The form to apply bounds to.</param>
    public void ApplyWindowBounds(Form form)
    {
        ArgumentNullException.ThrowIfNull(form);

        Settings.ValidateWindowBounds();

        if (Settings.MainWindowX >= 0 && Settings.MainWindowY >= 0)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Settings.MainWindowX, Settings.MainWindowY);
            form.Size = new Size(Settings.MainWindowWidth, Settings.MainWindowHeight);
        }
        else
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new Size(Settings.MainWindowWidth, Settings.MainWindowHeight);
        }

        if (Settings.MainWindowState != FormWindowState.Minimized)
        {
            form.WindowState = Settings.MainWindowState;
        }
    }
}
