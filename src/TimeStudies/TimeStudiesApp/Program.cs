using System.Globalization;
using TimeStudiesApp.Models;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Load settings to get language preference
        SettingsService settingsService = new();
        ApplicationSettings settings = settingsService.LoadSettings();

        // Apply culture from settings
        ApplyCulture(settings.UiLanguage);

        // Standard WinForms initialization
        ApplicationConfiguration.Initialize();

        // Enable DarkMode following system settings (.NET 9+)
        Application.SetColorMode(SystemColorMode.System);

        // Run the application
        Application.Run(new FrmMain());
    }

    /// <summary>
    /// Applies the specified culture to the current thread.
    /// </summary>
    private static void ApplyCulture(string language)
    {
        if (string.IsNullOrEmpty(language))
        {
            return;
        }

        try
        {
            CultureInfo culture = new(language);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }
        catch (CultureNotFoundException)
        {
            // Use default culture if specified culture is not found
        }
    }
}