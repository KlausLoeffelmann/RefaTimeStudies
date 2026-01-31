using TimeStudiesApp.Properties;
using TimeStudiesApp.Services;

namespace TimeStudiesApp.Dialogs;

/// <summary>
///  Dialog for editing application settings.
/// </summary>
public partial class SettingsDialog : Form
{
    private readonly SettingsService _settingsService;
    private readonly string _originalLanguage;

    /// <summary>
    ///  Initializes a new instance of the <see cref="SettingsDialog"/> class.
    /// </summary>
    /// <param name="settingsService">The settings service.</param>
    public SettingsDialog(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);

        _settingsService = settingsService;
        _originalLanguage = settingsService.Settings.UiLanguage;

        InitializeComponent();
        ApplyLocalization();
        LoadSettings();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        Text = Resources.SettingsTitle;
        _lblLanguage.Text = Resources.SettingsLanguage;
        _lblBaseDirectory.Text = Resources.SettingsBaseDir;
        _lblButtonSize.Text = Resources.SettingsButtonSize;
        _chkAutoSave.Text = Resources.SettingsAutoSave;
        _chkConfirmClose.Text = Resources.SettingsConfirmClose;
        _lblCsvDelimiter.Text = Resources.SettingsCsvDelimiter;
        _btnBrowse.Text = Resources.BtnBrowse;
        _btnOK.Text = Resources.BtnOK;
        _btnCancel.Text = Resources.BtnCancel;
    }

    /// <summary>
    ///  Loads current settings into the form.
    /// </summary>
    private void LoadSettings()
    {
        var settings = _settingsService.Settings;

        // Language
        _cboLanguage.Items.Clear();
        _cboLanguage.Items.Add(new LanguageItem("English", "en-US"));
        _cboLanguage.Items.Add(new LanguageItem("Deutsch", "de-DE"));

        for (int i = 0; i < _cboLanguage.Items.Count; i++)
        {
            if (_cboLanguage.Items[i] is LanguageItem item && item.Code == settings.UiLanguage)
            {
                _cboLanguage.SelectedIndex = i;
                break;
            }
        }

        if (_cboLanguage.SelectedIndex < 0 && _cboLanguage.Items.Count > 0)
        {
            _cboLanguage.SelectedIndex = 0;
        }

        // Base directory
        _txtBaseDirectory.Text = settings.BaseDirectory;

        // Button size
        _numButtonSize.Value = Math.Clamp(settings.ButtonSize, (int)_numButtonSize.Minimum, (int)_numButtonSize.Maximum);

        // Auto-save
        _chkAutoSave.Checked = settings.AutoSaveRecordings;

        // Confirm close
        _chkConfirmClose.Checked = settings.ConfirmBeforeClosingRecording;

        // CSV delimiter
        _cboCsvDelimiter.Items.Clear();
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Semicolon (;)", ";"));
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Comma (,)", ","));
        _cboCsvDelimiter.Items.Add(new DelimiterItem("Tab", "\t"));

        for (int i = 0; i < _cboCsvDelimiter.Items.Count; i++)
        {
            if (_cboCsvDelimiter.Items[i] is DelimiterItem item && item.Value == settings.CsvDelimiter)
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

    /// <summary>
    ///  Saves settings from the form.
    /// </summary>
    private void SaveSettings()
    {
        var settings = _settingsService.Settings;

        // Language
        if (_cboLanguage.SelectedItem is LanguageItem langItem)
        {
            settings.UiLanguage = langItem.Code;
        }

        // Base directory
        settings.BaseDirectory = _txtBaseDirectory.Text;

        // Button size
        settings.ButtonSize = (int)_numButtonSize.Value;

        // Auto-save
        settings.AutoSaveRecordings = _chkAutoSave.Checked;

        // Confirm close
        settings.ConfirmBeforeClosingRecording = _chkConfirmClose.Checked;

        // CSV delimiter
        if (_cboCsvDelimiter.SelectedItem is DelimiterItem delimItem)
        {
            settings.CsvDelimiter = delimItem.Value;
        }

        _settingsService.Save();
    }

    #region Event Handlers

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = Resources.TitleSelectFolder,
            UseDescriptionForTitle = true,
            SelectedPath = _txtBaseDirectory.Text
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _txtBaseDirectory.Text = dialog.SelectedPath;
        }
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        // Check if language changed
        if (_cboLanguage.SelectedItem is LanguageItem langItem && langItem.Code != _originalLanguage)
        {
            MessageBox.Show(
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

    #endregion

    #region Helper Classes

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

    #endregion
}
