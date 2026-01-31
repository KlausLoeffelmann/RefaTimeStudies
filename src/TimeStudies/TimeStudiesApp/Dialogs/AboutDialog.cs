using System.Reflection;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Dialogs;

/// <summary>
///  About dialog showing application information.
/// </summary>
public partial class AboutDialog : Form
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="AboutDialog"/> class.
    /// </summary>
    public AboutDialog()
    {
        InitializeComponent();
        ApplyLocalization();
        LoadVersionInfo();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        Text = Resources.TitleAbout;
        _btnOK.Text = Resources.BtnOK;
    }

    /// <summary>
    ///  Loads version information from assembly.
    /// </summary>
    private void LoadVersionInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version ?? new Version(1, 0, 0);

        _lblAppName.Text = Resources.AppTitle;
        _lblVersion.Text = $"Version {version.Major}.{version.Minor}.{version.Build}";
        _lblCopyright.Text = $"© {DateTime.Now.Year} REFA Time Study";
        _lblDescription.Text = "Industrial time study application for REFA-style measurements.\n\n" +
                               "Designed for touch-enabled devices with support for German and English localization.";
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
