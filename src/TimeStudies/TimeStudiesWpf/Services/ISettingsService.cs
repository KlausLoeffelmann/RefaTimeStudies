using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Interface for application settings management.
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Loads the application settings.
    /// </summary>
    ApplicationSettings Load();

    /// <summary>
    /// Saves the application settings.
    /// </summary>
    void Save(ApplicationSettings settings);

    /// <summary>
    /// Gets the current settings (cached).
    /// </summary>
    ApplicationSettings Current { get; }

    /// <summary>
    /// Ensures the base directories exist.
    /// </summary>
    void EnsureDirectoriesExist();
}
