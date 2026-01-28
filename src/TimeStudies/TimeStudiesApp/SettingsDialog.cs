using System.Globalization;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp;

/// <summary>
/// Dialog for configuring application settings.
/// </summary>
public partial class SettingsDialog : Form
{
    private readonly SettingsService _settingsService;
    private readonly string _originalLanguage;

    public SettingsDialog(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
        _originalLanguage = settingsService.Settings.UiLanguage;

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

        _lblLanguage.Text = Resources.SettingsLanguage + ":";
        _lblBaseDir.Text = Resources.SettingsBaseDir + ":";
        _lblButtonSize.Text = Resources.SettingsButtonSize + ":";

        _chkAutoSave.Text = Resources.SettingsAutoSave;
        _chkConfirmClose.Text = Resources.SettingsConfirmClose;

        _lblCsvDelimiter.Text = Resources.SettingsCsvDelimiter + ":";

        _btnBrowse.Text = Resources.BtnBrowse;
        _btnOK.Text = Resources.BtnOK;
        _btnCancel.Text = Resources.BtnCancel;
    }

    private void LoadSettings()
    {
        ApplicationSettings settings = _settingsService.Settings;

        // Language options
        _cboLanguage.Items.Clear();
        _cboLanguage.Items.Add(new LanguageItem("English", "en-US"));
        _cboLanguage.Items.Add(new LanguageItem("Deutsch", "de-DE"));

        for (int i = 0; i < _cboLanguage.Items.Count; i++)
        {
            if (_cboLanguage.Items[i] is LanguageItem item &&
                item.CultureCode.Equals(settings.UiLanguage, StringComparison.OrdinalIgnoreCase))
            {
                _cboLanguage.SelectedIndex = i;
                break;
            }
        }

        if (_cboLanguage.SelectedIndex < 0 && _cboLanguage.Items.Count > 0)
        {
            _cboLanguage.SelectedIndex = 0;
        }

        _txtBaseDir.Text = settings.BaseDirectory;
        _numButtonSize.Value = Math.Clamp(settings.ButtonSize, (int)_numButtonSize.Minimum, (int)_numButtonSize.Maximum);

        _chkAutoSave.Checked = settings.AutoSaveRecordings;
        _chkConfirmClose.Checked = settings.ConfirmBeforeClosingRecording;

        // CSV delimiter options
        _cboCsvDelimiter.Items.Clear();
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Semicolon (;)", ";"));
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Comma (,)", ","));
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Tab", "\t"));

        for (int i = 0; i < _cboCsvDelimiter.Items.Count; i++)
        {
            if (_cboCsvDelimiter.Items[i] is DelimiterItem item &&
                item.Delimiter.Equals(settings.CsvDelimiter, StringComparison.Ordinal))
            {
                _cboCsvDelimiter.SelectedIndex = i;
                break;
            }
        }

        if (_cboCsvDelimiter.SelectedIndex < 0 && _cboCsvDelimiter.Items.Count > 0)
        {
            _cboCsvDelimiter.SelectedIndex = 0;
        }
    }

    private void SaveSettings()
    {
        ApplicationSettings settings = _settingsService.Settings;

        if (_cboLanguage.SelectedItem is LanguageItem languageItem)
        {
            settings.UiLanguage = languageItem.CultureCode;
        }

        settings.BaseDirectory = _txtBaseDir.Text.Trim();
        settings.ButtonSize = (int)_numButtonSize.Value;
        settings.AutoSaveRecordings = _chkAutoSave.Checked;
        settings.ConfirmBeforeClosingRecording = _chkConfirmClose.Checked;

        if (_cboCsvDelimiter.SelectedItem is DelimiterItem delimiterItem)
        {
            settings.CsvDelimiter = delimiterItem.Delimiter;
        }

        settings.EnsureDirectoriesExist();
        _settingsService.SaveSettings();
    }

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = Resources.TitleSelectFolder,
            UseDescriptionForTitle = true,
            SelectedPath = _txtBaseDir.Text,
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _txtBaseDir.Text = dialog.SelectedPath;
        }
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        // Check if language changed
        if (!_originalLanguage.Equals(_settingsService.Settings.UiLanguage, StringComparison.OrdinalIgnoreCase))
        {
            // Apply the new culture for the current session
            CultureInfo newCulture = new(_settingsService.Settings.UiLanguage);
            CultureInfo.CurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

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

    /// <summary>
    /// Represents a language option in the dropdown.
    /// </summary>
    private sealed class LanguageItem
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

    /// <summary>
    /// Represents a delimiter option in the dropdown.
    /// </summary>
    private sealed class DelimiterItem
    {
        public string DisplayName { get; }
        public string Delimiter { get; }

        public DelimiterItem(string displayName, string delimiter)
        {
            DisplayName = displayName;
            Delimiter = delimiter;
        }

        public override string ToString() => DisplayName;
    }
}
