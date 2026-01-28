using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

/// <summary>
/// Settings dialog for configuring application preferences.
/// </summary>
public partial class SettingsDialog : Form
{
    private readonly SettingsService _settingsService;
    private readonly ApplicationSettings _originalSettings;
    private bool _languageChanged;

    public SettingsDialog(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
        _originalSettings = settingsService.Settings;

        InitializeComponent();
        ApplyLocalization();
        LoadSettings();
    }

    private void ApplyLocalization()
    {
        Text = Resources.SettingsTitle;

        _tabGeneral.Text = Resources.SettingsGeneral;
        _tabRecording.Text = Resources.SettingsRecording;
        _tabExport.Text = Resources.SettingsExport;

        _lblLanguage.Text = Resources.SettingsLanguage;
        _lblBaseDirectory.Text = Resources.SettingsBaseDir;
        _lblButtonSize.Text = Resources.SettingsButtonSize;

        _chkAutoSave.Text = Resources.SettingsAutoSave;
        _chkConfirmClose.Text = Resources.SettingsConfirmClose;

        _lblCsvDelimiter.Text = Resources.SettingsCsvDelimiter;

        _btnBrowse.Text = Resources.BtnBrowse;
        _btnOK.Text = Resources.BtnOK;
        _btnCancel.Text = Resources.BtnCancel;

        // Populate language dropdown
        _cboLanguage.Items.Clear();
        _cboLanguage.Items.Add(new LanguageItem("English", "en-US"));
        _cboLanguage.Items.Add(new LanguageItem("Deutsch", "de-DE"));

        // Populate CSV delimiter dropdown
        _cboDelimiter.Items.Clear();
        _cboDelimiter.Items.Add(new DelimiterItem("; (Semicolon)", ";"));
        _cboDelimiter.Items.Add(new DelimiterItem(", (Comma)", ","));
        _cboDelimiter.Items.Add(new DelimiterItem("Tab", "\t"));
    }

    private void LoadSettings()
    {
        // Language
        for (int i = 0; i < _cboLanguage.Items.Count; i++)
        {
            if (_cboLanguage.Items[i] is LanguageItem item &&
                item.Code == _originalSettings.UiLanguage)
            {
                _cboLanguage.SelectedIndex = i;
                break;
            }
        }

        if (_cboLanguage.SelectedIndex < 0)
        {
            _cboLanguage.SelectedIndex = 0;
        }

        // Base directory
        _txtBaseDirectory.Text = _originalSettings.BaseDirectory;

        // Button size
        _numButtonSize.Value = Math.Clamp(_originalSettings.ButtonSize, 48, 120);

        // Recording options
        _chkAutoSave.Checked = _originalSettings.AutoSaveRecordings;
        _chkConfirmClose.Checked = _originalSettings.ConfirmBeforeClosingRecording;

        // CSV delimiter
        for (int i = 0; i < _cboDelimiter.Items.Count; i++)
        {
            if (_cboDelimiter.Items[i] is DelimiterItem item &&
                item.Value == _originalSettings.CsvDelimiter)
            {
                _cboDelimiter.SelectedIndex = i;
                break;
            }
        }

        if (_cboDelimiter.SelectedIndex < 0)
        {
            _cboDelimiter.SelectedIndex = 0;
        }
    }

    private void SaveSettings()
    {
        ApplicationSettings settings = _settingsService.Settings;

        // Language
        if (_cboLanguage.SelectedItem is LanguageItem langItem)
        {
            if (settings.UiLanguage != langItem.Code)
            {
                settings.UiLanguage = langItem.Code;
                _languageChanged = true;
            }
        }

        // Base directory
        settings.BaseDirectory = _txtBaseDirectory.Text;

        // Button size
        settings.ButtonSize = (int)_numButtonSize.Value;

        // Recording options
        settings.AutoSaveRecordings = _chkAutoSave.Checked;
        settings.ConfirmBeforeClosingRecording = _chkConfirmClose.Checked;

        // CSV delimiter
        if (_cboDelimiter.SelectedItem is DelimiterItem delimItem)
        {
            settings.CsvDelimiter = delimItem.Value;
        }

        _settingsService.SaveSettings();
    }

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = Resources.TitleSelectFolder,
            UseDescriptionForTitle = true,
            SelectedPath = _txtBaseDirectory.Text,
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _txtBaseDirectory.Text = dialog.SelectedPath;
        }
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        if (_languageChanged)
        {
            MessageBox.Show(
                this,
                Resources.MsgRestartRequired,
                Resources.TitleInformation,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    /// Represents a language option in the dropdown.
    /// </summary>
    private sealed class LanguageItem
    {
        public string DisplayName { get; }
        public string Code { get; }

        public LanguageItem(string displayName, string code)
        {
            DisplayName = displayName;
            Code = code;
        }

        public override string ToString() => DisplayName;
    }

    /// <summary>
    /// Represents a delimiter option in the dropdown.
    /// </summary>
    private sealed class DelimiterItem
    {
        public string DisplayName { get; }
        public string Value { get; }

        public DelimiterItem(string displayName, string value)
        {
            DisplayName = displayName;
            Value = value;
        }

        public override string ToString() => DisplayName;
    }
}
