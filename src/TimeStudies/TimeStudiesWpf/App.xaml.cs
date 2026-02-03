using System.Globalization;
using System.Windows;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static ISettingsService SettingsService { get; private set; } = null!;
    public static IDefinitionService DefinitionService { get; private set; } = null!;
    public static IRecordingService RecordingService { get; private set; } = null!;
    public static ICsvExportService CsvExportService { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Initialize services
        SettingsService = new SettingsService();
        DefinitionService = new DefinitionService(SettingsService);
        RecordingService = new RecordingService(SettingsService);
        CsvExportService = new CsvExportService();

        // Ensure directories exist
        SettingsService.EnsureDirectoriesExist();

        // Apply language from settings
        var settings = SettingsService.Load();
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.UiLanguage);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(settings.UiLanguage);

        base.OnStartup(e);
    }
}
