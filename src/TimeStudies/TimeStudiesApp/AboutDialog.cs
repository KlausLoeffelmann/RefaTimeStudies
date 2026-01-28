using System.Reflection;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp;

/// <summary>
/// About dialog displaying application information.
/// </summary>
public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
        ApplyLocalization();
        LoadApplicationInfo();
    }

    private void ApplyLocalization()
    {
        Text = Resources.AboutTitle;
        _lblAppName.Text = Resources.AppTitle;
        _lblDescription.Text = Resources.AboutDescription;
        _btnOK.Text = Resources.BtnOK;
    }

    private void LoadApplicationInfo()
    {
        // Get version from assembly
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        string versionString = version is not null
            ? $"{version.Major}.{version.Minor}.{version.Build}"
            : "1.0.0";

        _lblVersion.Text = $"{Resources.AboutVersion}: {versionString}";

        // Create icon using SymbolIconFactory
        _picIcon.Image = SymbolIconFactory.RenderSymbol(
            SymbolIconFactory.Symbols.Clock,
            new Size(64, 64),
            Application.IsDarkModeEnabled ? Color.FromArgb(100, 149, 237) : Color.FromArgb(0, 102, 204));
    }
}
