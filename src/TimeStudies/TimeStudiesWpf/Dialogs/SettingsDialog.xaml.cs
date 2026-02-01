using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WpfMessageBox = System.Windows.MessageBox;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.Dialogs;

/// <summary>
/// Interaction logic for SettingsDialog.xaml
/// </summary>
public partial class SettingsDialog : Window
{
    private readonly SettingsService _settingsService;
    private ApplicationSettings _settings;
    private bool _languageChanged;

    public SettingsDialog()
    {
        InitializeComponent();
        _settingsService = App.SettingsService;
        _settings = _settingsService.Settings;
        _languageChanged = false;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        SetLocalizedStrings();
        LoadSettings();
    }

    private void SetLocalizedStrings()
    {
        Title = ResourceHelper.GetString("TitleSettings");
        HeaderLabel.Content = ResourceHelper.GetString("SettingsLanguage");
        LanguageLabel.Content = ResourceHelper.GetString("SettingsLanguage");
        LanguageChoiceLabel.Content = ResourceHelper.GetString("Language") + ":";
        LanguageWarningTextBlock.Text = ResourceHelper.GetString("MsgRestartRequired");
        BaseDirectoryLabel.Content = ResourceHelper.GetString("SettingsBaseDir");
        ButtonSizeLabel.Content = ResourceHelper.GetString("SettingsButtonSize");
        ButtonSizeChoiceLabel.Content = ResourceHelper.GetString("ButtonSize") + ":";
        AutoSaveLabel.Content = ResourceHelper.GetString("AutoSave");
        ConfirmCloseLabel.Content = ResourceHelper.GetString("ConfirmClose");
        AutoSaveCheckBox.Content = ResourceHelper.GetString("SettingsAutoSave");
        ConfirmCloseCheckBox.Content = ResourceHelper.GetString("SettingsConfirmClose");
        OKButton.Content = ResourceHelper.GetString("BtnOK");
        CancelButton.Content = ResourceHelper.GetString("BtnCancel");
        BrowseDirectoryButton.Content = "...";

        // Populate language dropdown
        LanguageComboBox.Items.Clear();
        LanguageComboBox.Items.Add(new ComboBoxItem { Content = "English (en-US)", Tag = "en-US" });
        LanguageComboBox.Items.Add(new ComboBoxItem { Content = "Deutsch (de-DE)", Tag = "de-DE" });
    }

    private void LoadSettings()
    {
        LanguageComboBox.SelectedValue = _settings.UiLanguage;
        BaseDirectoryTextBox.Text = _settings.BaseDirectory;
        ButtonSizeSlider.Value = _settings.ButtonSize;
        ButtonSizeValueTextBlock.Text = _settings.ButtonSize + " px";
        AutoSaveCheckBox.IsChecked = _settings.AutoSaveRecordings;
        ConfirmCloseCheckBox.IsChecked = _settings.ConfirmBeforeClosingRecording;
    }

    private void BrowseDirectory_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = ResourceHelper.GetString("TitleSelectFolder"),
            SelectedPath = BaseDirectoryTextBox.Text
        };

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            BaseDirectoryTextBox.Text = dialog.SelectedPath;
        }
    }

    private void ButtonSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ButtonSizeValueTextBlock.Text = ((int)e.NewValue).ToString() + " px";
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LanguageComboBox.SelectedItem is ComboBoxItem item)
        {
            var selectedLanguage = item.Tag as string;
            if (selectedLanguage != _settings.UiLanguage)
            {
                _languageChanged = true;
            }
        }
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (LanguageComboBox.SelectedItem is ComboBoxItem item)
            {
                var newLanguage = item.Tag as string;
                if (newLanguage != null && newLanguage != _settings.UiLanguage)
                {
                    _settingsService.SetLanguage(newLanguage);
                    _languageChanged = true;
                }
            }

            if (!string.IsNullOrEmpty(BaseDirectoryTextBox.Text))
            {
                _settingsService.SetBaseDirectory(BaseDirectoryTextBox.Text);
            }

            _settingsService.SetButtonSize((int)ButtonSizeSlider.Value);
            _settingsService.SetAutoSave(AutoSaveCheckBox.IsChecked ?? true);
            _settingsService.SetConfirmBeforeClosing(ConfirmCloseCheckBox.IsChecked ?? true);

            DialogResult = true;

            if (_languageChanged)
            {
                WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgRestartRequired"),
                    "Information",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
            }

            Close();
        }
        catch (Exception ex)
        {
            WpfMessageBox.Show(
                $"Failed to save settings: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}