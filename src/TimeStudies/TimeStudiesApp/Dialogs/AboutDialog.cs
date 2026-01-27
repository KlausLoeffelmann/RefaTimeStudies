using System.Reflection;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Dialogs;

/// <summary>
/// About dialog showing application information.
/// </summary>
public partial class AboutDialog : Form
{
    public AboutDialog()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        // Form settings
        Text = Resources.AboutTitle;
        Size = new Size(450, 300);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        BackColor = Color.White;

        // Icon/Logo
        var iconSize = 64;
        _picIcon = new PictureBox
        {
            Location = new Point(30, 30),
            Size = new Size(iconSize, iconSize),
            SizeMode = PictureBoxSizeMode.CenterImage,
            Image = IconFactory.CreateIcon(IconFactory.Icons.Timer, iconSize, Color.FromArgb(0, 120, 212))
        };

        // App title
        _lblTitle = new Label
        {
            Text = Resources.AppTitle,
            Location = new Point(110, 30),
            Size = new Size(300, 30),
            Font = new Font(Font.FontFamily, 16, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 120, 212)
        };

        // Version
        var version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);
        _lblVersion = new Label
        {
            Text = $"{Resources.AboutVersion}: {version.Major}.{version.Minor}.{version.Build}",
            Location = new Point(110, 65),
            Size = new Size(300, 20),
            ForeColor = Color.DimGray
        };

        // Description
        _lblDescription = new Label
        {
            Text = Resources.AboutDescription,
            Location = new Point(110, 95),
            Size = new Size(300, 50),
            ForeColor = Color.Black
        };

        // Separator
        _separator = new Panel
        {
            Location = new Point(30, 170),
            Size = new Size(380, 1),
            BackColor = Color.LightGray
        };

        // Copyright
        _lblCopyright = new Label
        {
            Text = Resources.AboutCopyright,
            Location = new Point(30, 185),
            Size = new Size(380, 20),
            ForeColor = Color.DimGray,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // OK Button
        _btnOk = new Button
        {
            Text = Resources.BtnOk,
            Size = new Size(90, 30),
            Location = new Point((450 - 90) / 2, 215),
            DialogResult = DialogResult.OK
        };

        Controls.AddRange([_picIcon, _lblTitle, _lblVersion, _lblDescription, _separator, _lblCopyright, _btnOk]);

        AcceptButton = _btnOk;

        ResumeLayout(true);
    }

    // Controls
    private PictureBox _picIcon = null!;
    private Label _lblTitle = null!;
    private Label _lblVersion = null!;
    private Label _lblDescription = null!;
    private Panel _separator = null!;
    private Label _lblCopyright = null!;
    private Button _btnOk = null!;
}
