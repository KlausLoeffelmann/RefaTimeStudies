using System.Reflection;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp;

/// <summary>
/// About dialog showing application information.
/// </summary>
public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
        ApplyLocalization();
        LoadAppInfo();
    }

    private void ApplyLocalization()
    {
        Text = Resources.AboutTitle;
        _lblCopyright.Text = Resources.AboutCopyright;
        _lblDescription.Text = Resources.AboutDescription;
        _btnOK.Text = Resources.BtnOK;
    }

    private void LoadAppInfo()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        string versionText = version is not null
            ? $"{Resources.AboutVersion}: {version.Major}.{version.Minor}.{version.Build}"
            : $"{Resources.AboutVersion}: 1.0.0";

        _lblVersion.Text = versionText;
        _lblAppName.Text = Resources.AppTitle;

        // Set app icon using SymbolFactory
        _picIcon.Image = SymbolFactory.RenderSymbol(
            SymbolFactory.Symbols.Timer,
            64,
            SystemColors.Highlight);
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
