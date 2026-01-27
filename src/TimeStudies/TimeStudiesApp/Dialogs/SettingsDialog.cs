using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Dialogs;

/// <summary>
/// Settings dialog for configuring application options.
/// </summary>
public partial class SettingsDialog : Form
{
    private readonly SettingsService _settingsService;
    private readonly string _originalLanguage;

    public SettingsDialog(SettingsService settingsService)
    {
        _settingsService = settingsService;
        _originalLanguage = settingsService.CurrentSettings.UiLanguage;
        
        InitializeComponent();
        ApplyLocalization();
        LoadSettings();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        // Form settings
        Text = Resources.SettingsTitle;
        Size = new Size(500, 400);
        MinimumSize = new Size(400, 350);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;

        // Tab control
        _tabControl = new TabControl
        {
            Dock = DockStyle.Fill,
            Padding = new Point(10, 5)
        };

        // General tab
        _tabGeneral = new TabPage { Text = Resources.SettingsGeneral, Padding = new Padding(15) };
        CreateGeneralTab();
        _tabControl.TabPages.Add(_tabGeneral);

        // Recording tab
        _tabRecording = new TabPage { Text = Resources.SettingsRecording, Padding = new Padding(15) };
        CreateRecordingTab();
        _tabControl.TabPages.Add(_tabRecording);

        // Export tab
        _tabExport = new TabPage { Text = Resources.SettingsExport, Padding = new Padding(15) };
        CreateExportTab();
        _tabControl.TabPages.Add(_tabExport);

        // Button panel
        _panelButtons = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            Padding = new Padding(10)
        };

        _btnOk = new Button
        {
            Text = Resources.BtnOk,
            Size = new Size(90, 30),
            DialogResult = DialogResult.OK
        };
        _btnOk.Click += BtnOk_Click;

        _btnCancel = new Button
        {
            Text = Resources.BtnCancel,
            Size = new Size(90, 30),
            DialogResult = DialogResult.Cancel
        };

        // Position buttons
        _btnCancel.Location = new Point(_panelButtons.Width - _btnCancel.Width - 15, 10);
        _btnOk.Location = new Point(_btnCancel.Left - _btnOk.Width - 10, 10);
        _btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        _btnOk.Anchor = AnchorStyles.Right | AnchorStyles.Top;

        _panelButtons.Controls.AddRange([_btnOk, _btnCancel]);

        Controls.Add(_tabControl);
        Controls.Add(_panelButtons);

        AcceptButton = _btnOk;
        CancelButton = _btnCancel;

        ResumeLayout(true);
    }

    private void CreateGeneralTab()
    {
        var y = 10;
        const int labelWidth = 140;
        const int controlWidth = 280;
        const int rowHeight = 35;

        // Language
        var lblLanguage = new Label
        {
            Text = Resources.SettingsLanguage + ":",
            Location = new Point(10, y + 3),
            Size = new Size(labelWidth, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _cboLanguage = new ComboBox
        {
            Location = new Point(labelWidth + 20, y),
            Size = new Size(controlWidth, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cboLanguage.Items.Add(new LanguageItem("English", "en-US"));
        _cboLanguage.Items.Add(new LanguageItem("Deutsch", "de-DE"));
        _cboLanguage.DisplayMember = "DisplayName";

        _tabGeneral.Controls.AddRange([lblLanguage, _cboLanguage]);
        y += rowHeight + 5;

        // Base Directory
        var lblBaseDir = new Label
        {
            Text = Resources.SettingsBaseDir + ":",
            Location = new Point(10, y + 3),
            Size = new Size(labelWidth, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _txtBaseDir = new TextBox
        {
            Location = new Point(labelWidth + 20, y),
            Size = new Size(controlWidth - 80, 25),
            ReadOnly = true
        };

        _btnBrowse = new Button
        {
            Text = "...",
            Location = new Point(labelWidth + 20 + controlWidth - 70, y),
            Size = new Size(70, 25)
        };
        _btnBrowse.Click += BtnBrowse_Click;

        _tabGeneral.Controls.AddRange([lblBaseDir, _txtBaseDir, _btnBrowse]);
        y += rowHeight + 5;

        // Button Size
        var lblButtonSize = new Label
        {
            Text = Resources.SettingsButtonSize + ":",
            Location = new Point(10, y + 3),
            Size = new Size(labelWidth, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _numButtonSize = new NumericUpDown
        {
            Location = new Point(labelWidth + 20, y),
            Size = new Size(100, 25),
            Minimum = 40,
            Maximum = 120,
            Increment = 10,
            Value = 60
        };

        var lblPixels = new Label
        {
            Text = "px",
            Location = new Point(labelWidth + 125, y + 3),
            Size = new Size(30, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _tabGeneral.Controls.AddRange([lblButtonSize, _numButtonSize, lblPixels]);
    }

    private void CreateRecordingTab()
    {
        var y = 10;

        // Auto-save
        _chkAutoSave = new CheckBox
        {
            Text = Resources.SettingsAutoSave,
            Location = new Point(10, y),
            Size = new Size(400, 25),
            Checked = true
        };
        y += 35;

        // Confirm before stop
        _chkConfirmStop = new CheckBox
        {
            Text = Resources.SettingsConfirmStop,
            Location = new Point(10, y),
            Size = new Size(400, 25),
            Checked = true
        };

        _tabRecording.Controls.AddRange([_chkAutoSave, _chkConfirmStop]);
    }

    private void CreateExportTab()
    {
        var y = 10;
        const int labelWidth = 140;

        // CSV Delimiter
        var lblDelimiter = new Label
        {
            Text = Resources.SettingsCsvDelimiter + ":",
            Location = new Point(10, y + 3),
            Size = new Size(labelWidth, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _cboDelimiter = new ComboBox
        {
            Location = new Point(labelWidth + 20, y),
            Size = new Size(150, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cboDelimiter.Items.Add(new DelimiterItem("; (Semicolon)", ";"));
        _cboDelimiter.Items.Add(new DelimiterItem(", (Comma)", ","));
        _cboDelimiter.Items.Add(new DelimiterItem("Tab", "\t"));
        _cboDelimiter.DisplayMember = "DisplayName";

        _tabExport.Controls.AddRange([lblDelimiter, _cboDelimiter]);
    }

    private void ApplyLocalization()
    {
        Text = Resources.SettingsTitle;
        _tabGeneral.Text = Resources.SettingsGeneral;
        _tabRecording.Text = Resources.SettingsRecording;
        _tabExport.Text = Resources.SettingsExport;
        _btnOk.Text = Resources.BtnOk;
        _btnCancel.Text = Resources.BtnCancel;
    }

    private void LoadSettings()
    {
        var settings = _settingsService.CurrentSettings;

        // Language
        foreach (LanguageItem item in _cboLanguage.Items)
        {
            if (item.Code == settings.UiLanguage)
            {
                _cboLanguage.SelectedItem = item;
                break;
            }
        }
        if (_cboLanguage.SelectedIndex < 0 && _cboLanguage.Items.Count > 0)
            _cboLanguage.SelectedIndex = 0;

        // Base directory
        _txtBaseDir.Text = settings.BaseDirectory;

        // Button size
        _numButtonSize.Value = Math.Clamp(settings.ButtonSize, (int)_numButtonSize.Minimum, (int)_numButtonSize.Maximum);

        // Recording settings
        _chkAutoSave.Checked = settings.AutoSaveRecordings;
        _chkConfirmStop.Checked = settings.ConfirmBeforeClosingRecording;

        // CSV delimiter
        foreach (DelimiterItem item in _cboDelimiter.Items)
        {
            if (item.Value == settings.CsvDelimiter)
            {
                _cboDelimiter.SelectedItem = item;
                break;
            }
        }
        if (_cboDelimiter.SelectedIndex < 0 && _cboDelimiter.Items.Count > 0)
            _cboDelimiter.SelectedIndex = 0;
    }

    private void SaveSettings()
    {
        var settings = _settingsService.CurrentSettings;

        // Language
        if (_cboLanguage.SelectedItem is LanguageItem langItem)
        {
            settings.UiLanguage = langItem.Code;
        }

        // Base directory
        settings.BaseDirectory = _txtBaseDir.Text;

        // Button size
        settings.ButtonSize = (int)_numButtonSize.Value;

        // Recording settings
        settings.AutoSaveRecordings = _chkAutoSave.Checked;
        settings.ConfirmBeforeClosingRecording = _chkConfirmStop.Checked;

        // CSV delimiter
        if (_cboDelimiter.SelectedItem is DelimiterItem delimItem)
        {
            settings.CsvDelimiter = delimItem.Value;
        }

        _settingsService.Save(settings);
    }

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = Resources.TitleSelectFolder,
            SelectedPath = _txtBaseDir.Text,
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _txtBaseDir.Text = dialog.SelectedPath;
        }
    }

    private void BtnOk_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        // Check if language changed
        if (_settingsService.HasLanguageChanged(_originalLanguage))
        {
            MessageBox.Show(
                Resources.MsgRestartRequired,
                Resources.TitleInfo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }

    // Controls
    private TabControl _tabControl = null!;
    private TabPage _tabGeneral = null!;
    private TabPage _tabRecording = null!;
    private TabPage _tabExport = null!;
    private Panel _panelButtons = null!;
    private Button _btnOk = null!;
    private Button _btnCancel = null!;
    private ComboBox _cboLanguage = null!;
    private TextBox _txtBaseDir = null!;
    private Button _btnBrowse = null!;
    private NumericUpDown _numButtonSize = null!;
    private CheckBox _chkAutoSave = null!;
    private CheckBox _chkConfirmStop = null!;
    private ComboBox _cboDelimiter = null!;

    // Helper classes
    private class LanguageItem(string displayName, string code)
    {
        public string DisplayName { get; } = displayName;
        public string Code { get; } = code;
        public override string ToString() => DisplayName;
    }

    private class DelimiterItem(string displayName, string value)
    {
        public string DisplayName { get; } = displayName;
        public string Value { get; } = value;
        public override string ToString() => DisplayName;
    }
}
