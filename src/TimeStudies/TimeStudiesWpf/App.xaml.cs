using System.Windows;
using WpfApplication = System.Windows.Application;
using WpfMessageBox = System.Windows.MessageBox;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf
{
    /// <summary>
    /// Application entry point for the REFA Time Study application.
    /// Anwendungseinstiegspunkt für die REFA-Zeitaufnahme-Anwendung.
    /// </summary>
    public partial class App : WpfApplication
    {
        /// <summary>
        /// Gets the settings service instance.
        /// Ruft die SettingsService-Instanz ab.
        /// </summary>
        public static SettingsService SettingsService { get; private set; } = null!;

        /// <summary>
        /// Gets the definition service instance.
        /// Ruft die DefinitionService-Instanz ab.
        /// </summary>
        public static DefinitionService DefinitionService { get; private set; } = null!;

        /// <summary>
        /// Gets the recording service instance.
        /// Ruft die RecordingService-Instanz ab.
        /// </summary>
        public static RecordingService RecordingService { get; private set; } = null!;

        /// <summary>
        /// Gets the CSV export service instance.
        /// Ruft die CsvExportService-Instanz ab.
        /// </summary>
        public static CsvExportService CsvExportService { get; private set; } = null!;

        /// <summary>
        /// Handles application startup by initializing services and UI culture.
        /// Behandelt den Anwendungsstart, indem Dienste und UI-Kultur initialisiert werden.
        /// </summary>
        private void ApplicationStartup(object sender, System.Windows.StartupEventArgs e)
        {
            InitializeServices();
            ApplySettings();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        /// <summary>
        /// Initializes all application services.
        /// Initialisiert alle Anwendungsdienste.
        /// </summary>
        private void InitializeServices()
        {
            SettingsService = new SettingsService();
            var settings = SettingsService.Settings;

            DefinitionService = new DefinitionService(settings.BaseDirectory);
            RecordingService = new RecordingService(settings.BaseDirectory);
            CsvExportService = new CsvExportService(useSemicolonDelimiter: true);
        }

        /// <summary>
        /// Applies application settings, including UI culture.
        /// Wendet Anwendungseinstellungen an, einschließlich der UI-Kultur.
        /// </summary>
        private void ApplySettings()
        {
            var settings = SettingsService.Settings;
            ResourceHelper.SetCulture(settings.UiLanguage);
        }

        /// <summary>
        /// Handles unhandled exceptions at the application level.
        /// Behandelt unbehandelte Ausnahmen auf Anwendungsebene.
        /// </summary>
        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            WpfMessageBox.Show(
                $"An unexpected error occurred:\n{e.Exception.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}
