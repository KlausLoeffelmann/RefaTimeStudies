using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Dialogs;

/// <summary>
/// Interaction logic for SettingsDialog.xaml
/// </summary>
public partial class SettingsDialog : Window
{
    private readonly ApplicationSettings _settings;
    private readonly string _originalLanguage;

    public SettingsDialog()
    {
        InitializeComponent();

        _settings = App.SettingsService.Load();
        _originalLanguage = _settings.UiLanguage;

        // Initialize controls from settings
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Language
        foreach (ComboBoxItem item in LanguageCombo.Items)
        {
            if (item.Tag?.ToString() == _settings.UiLanguage)
            {
                LanguageCombo.SelectedItem = item;
                break;
            }
        }

        // Base Directory
        BaseDirectoryTextBox.Text = _settings.BaseDirectory;

        // Button Size
        ButtonSizeSlider.Value = _settings.ButtonSize;

        // Options
        ConfirmCloseCheckBox.IsChecked = _settings.ConfirmBeforeClosingRecording;
        AutoSaveCheckBox.IsChecked = _settings.AutoSaveRecordings;
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = "Select base directory for definitions and recordings",
            SelectedPath = _settings.BaseDirectory,
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            BaseDirectoryTextBox.Text = dialog.SelectedPath;
        }
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        // Save settings
        _settings.UiLanguage = (LanguageCombo.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "en-US";
        _settings.BaseDirectory = BaseDirectoryTextBox.Text;
        _settings.ButtonSize = (int)ButtonSizeSlider.Value;
        _settings.ConfirmBeforeClosingRecording = ConfirmCloseCheckBox.IsChecked == true;
        _settings.AutoSaveRecordings = AutoSaveCheckBox.IsChecked == true;

        App.SettingsService.Save(_settings);
        App.SettingsService.EnsureDirectoriesExist();

        // Check if language changed
        if (_settings.UiLanguage != _originalLanguage)
        {
            System.Windows.MessageBox.Show(
                "Please restart the application for language changes to take effect.\n\n" +
                "Bitte starten Sie die Anwendung neu, damit die Sprachänderungen wirksam werden.",
                "Restart Required / Neustart erforderlich",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information);
        }

        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
