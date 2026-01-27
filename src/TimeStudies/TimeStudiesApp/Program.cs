using System.Globalization;
using TimeStudiesApp.Services;

namespace TimeStudiesApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load settings and apply language before initializing the application
            var settingsService = new SettingsService();
            var settings = settingsService.Load();

            // Apply the language setting
            var culture = new CultureInfo(settings.UiLanguage);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            // Ensure directories exist
            settings.EnsureDirectoriesExist();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMain());
        }
    }
}