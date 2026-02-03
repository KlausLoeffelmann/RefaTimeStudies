using System.Globalization;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Set up global exception handling
        Application.ThreadException += OnThreadException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        // Initialize application configuration (High DPI, etc.)
        ApplicationConfiguration.Initialize();

        // Load settings and apply culture
        try
        {
            var settingsService = new SettingsService();
            var settings = settingsService.Load();
            ApplyCulture(settings.UiLanguage);
        }
        catch
        {
            // If settings can't be loaded, use system default
        }

        // Run the application
        Application.Run(new FrmMain());
    }

    /// <summary>
    /// Applies the specified culture to the current thread.
    /// </summary>
    private static void ApplyCulture(string cultureName)
    {
        if (string.IsNullOrEmpty(cultureName))
            return;

        try
        {
            var culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }
        catch
        {
            // If culture is invalid, use system default
        }
    }

    /// <summary>
    /// Handles exceptions on the UI thread.
    /// </summary>
    private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
        HandleException(e.Exception);
    }

    /// <summary>
    /// Handles unhandled exceptions from other threads.
    /// </summary>
    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            HandleException(ex);
        }
    }

    /// <summary>
    /// Shows an error message for the exception.
    /// </summary>
    private static void HandleException(Exception ex)
    {
        string message = $"An unexpected error occurred:\n\n{ex.Message}";

#if DEBUG
        message += $"\n\nStack Trace:\n{ex.StackTrace}";
#endif

        MessageBox.Show(
            message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
