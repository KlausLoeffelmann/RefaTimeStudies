using System.Reflection;

namespace TimeStudiesApp.Dialogs;

/// <summary>
/// About dialog showing application information.
/// </summary>
public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
        LoadVersionInfo();
        SetupEventHandlers();
    }

    private void LoadVersionInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version ?? new Version(1, 0, 0);

        _lblVersion.Text = $"Version {version.Major}.{version.Minor}.{version.Build}";

        var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
        if (copyrightAttr != null)
        {
            _lblCopyright.Text = copyrightAttr.Copyright;
        }
        else
        {
            _lblCopyright.Text = $"© {DateTime.Now.Year}";
        }
    }

    private void SetupEventHandlers()
    {
        _btnOK.Click += OnOKClick;
    }

    private void OnOKClick(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    /// <summary>
    /// Applies localization to the dialog.
    /// </summary>
    public void ApplyLocalization(
        string title,
        string appName,
        string description,
        string btnOK)
    {
        Text = title;
        _lblAppName.Text = appName;
        _lblDescription.Text = description;
        _btnOK.Text = btnOK;
    }
}
