using TimeStudiesApp.Models;

namespace TimeStudiesApp.Dialogs;

/// <summary>
/// Dialog for configuring application settings.
/// </summary>
public partial class SettingsDialog : Form
{
    private readonly ApplicationSettings _originalSettings;
    private bool _languageChanged;

    /// <summary>
    /// Gets the modified settings.
    /// </summary>
    public ApplicationSettings Settings { get; private set; }

    /// <summary>
    /// Gets whether the language was changed.
    /// </summary>
    public bool LanguageChanged => _languageChanged;

    public SettingsDialog(ApplicationSettings settings)
    {
        _originalSettings = settings;
        Settings = settings.Clone();

        InitializeComponent();
        SetupLanguageDropdown();
        LoadSettings();
        SetupEventHandlers();
    }

    private void SetupLanguageDropdown()
    {
        _cboLanguage.Items.Add(new LanguageItem("English", "en-US"));
        _cboLanguage.Items.Add(new LanguageItem("Deutsch", "de-DE"));
        _cboLanguage.DisplayMember = "DisplayName";
        _cboLanguage.ValueMember = "CultureCode";
    }

    private void LoadSettings()
    {
        // Select current language
        for (int i = 0; i < _cboLanguage.Items.Count; i++)
        {
            if (_cboLanguage.Items[i] is LanguageItem item &&
                item.CultureCode == Settings.UiLanguage)
            {
                _cboLanguage.SelectedIndex = i;
                break;
            }
        }

        if (_cboLanguage.SelectedIndex < 0)
            _cboLanguage.SelectedIndex = 0;

        _txtBaseDir.Text = Settings.BaseDirectory;
        _numButtonSize.Value = Settings.ButtonSize;
        _chkAutoSave.Checked = Settings.AutoSaveRecordings;
        _chkConfirmClose.Checked = Settings.ConfirmBeforeClosingRecording;
    }

    private void SetupEventHandlers()
    {
        _btnBrowse.Click += OnBrowseClick;
        _btnOK.Click += OnOKClick;
        _btnCancel.Click += OnCancelClick;
        _cboLanguage.SelectedIndexChanged += OnLanguageChanged;
    }

    private void OnBrowseClick(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select Base Directory",
            ShowNewFolderButton = true,
            SelectedPath = string.IsNullOrEmpty(_txtBaseDir.Text)
                ? ApplicationSettings.GetDefaultBaseDirectory()
                : _txtBaseDir.Text
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _txtBaseDir.Text = dialog.SelectedPath;
        }
    }

    private void OnLanguageChanged(object? sender, EventArgs e)
    {
        if (_cboLanguage.SelectedItem is LanguageItem item)
        {
            _languageChanged = item.CultureCode != _originalSettings.UiLanguage;
        }
    }

    private void OnOKClick(object? sender, EventArgs e)
    {
        // Update settings
        if (_cboLanguage.SelectedItem is LanguageItem item)
        {
            Settings.UiLanguage = item.CultureCode;
        }

        Settings.BaseDirectory = _txtBaseDir.Text;
        Settings.ButtonSize = (int)_numButtonSize.Value;
        Settings.AutoSaveRecordings = _chkAutoSave.Checked;
        Settings.ConfirmBeforeClosingRecording = _chkConfirmClose.Checked;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnCancelClick(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    /// Applies localization to the dialog.
    /// </summary>
    public void ApplyLocalization(
        string title,
        string lblLanguage,
        string lblBaseDir,
        string lblButtonSize,
        string chkAutoSave,
        string chkConfirmClose,
        string btnBrowse,
        string btnOK,
        string btnCancel)
    {
        Text = title;
        _lblLanguage.Text = lblLanguage + ":";
        _lblBaseDir.Text = lblBaseDir + ":";
        _lblButtonSize.Text = lblButtonSize + ":";
        _chkAutoSave.Text = chkAutoSave;
        _chkConfirmClose.Text = chkConfirmClose;
        _btnBrowse.Text = btnBrowse;
        _btnOK.Text = btnOK;
        _btnCancel.Text = btnCancel;
    }

    private class LanguageItem
    {
        public string DisplayName { get; }
        public string CultureCode { get; }

        public LanguageItem(string displayName, string cultureCode)
        {
            DisplayName = displayName;
            CultureCode = cultureCode;
        }

        public override string ToString() => DisplayName;
    }
}
