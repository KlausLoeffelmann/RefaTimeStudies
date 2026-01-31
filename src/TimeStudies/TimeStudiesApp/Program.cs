using System.Globalization;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

/// <summary>
///  Application entry point.
/// </summary>
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Initialize application configuration
        ApplicationConfiguration.Initialize();

        // Set high DPI mode
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

        // Enable DarkMode support (.NET 9+)
        Application.SetColorMode(SystemColorMode.System);

        // Set up global exception handling
        Application.ThreadException += OnThreadException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

        // Load settings
        SettingsService settingsService = new();
        settingsService.Load();

        // Apply language setting
        ApplyLanguage(settingsService.Settings.UiLanguage);

        // Run the main form
        Application.Run(new MainForm(settingsService));
    }

    /// <summary>
    ///  Applies the specified language culture to the application.
    /// </summary>
    /// <param name="languageCode">The language code (e.g., "en-US" or "de-DE").</param>
    private static void ApplyLanguage(string languageCode)
    {
        try
        {
            var culture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        catch (CultureNotFoundException)
        {
            // Fallback to default culture
            System.Diagnostics.Debug.WriteLine($"Culture '{languageCode}' not found, using default.");
        }
    }

    /// <summary>
    ///  Handles exceptions on the UI thread.
    /// </summary>
    private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
        LogException(e.Exception);
        MessageBox.Show(
            $"An error occurred: {e.Exception.Message}",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    /// <summary>
    ///  Handles unhandled exceptions on any thread.
    /// </summary>
    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    ///  Logs an exception for debugging.
    /// </summary>
    private static void LogException(Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Unhandled exception: {ex}");
    }
}