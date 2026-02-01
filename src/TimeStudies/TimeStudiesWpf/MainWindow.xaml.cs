using System.Windows;
using WpfMessageBox = System.Windows.MessageBox;
using TimeStudiesWpf.Controls;
using TimeStudiesWpf.Dialogs;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf
{
    /// <summary>
    /// Main window for the REFA Time Study application.
    /// Hauptfenster für die REFA-Zeitaufnahme-Anwendung.
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeStudyDefinition? _currentDefinition;
        private DefinitionEditorControl? _definitionEditor;
        private RecordingViewControl? _recordingView;
        private ResultsViewControl? _resultsView;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetLocalizedStrings();
            ShowWelcomeScreen();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check for unsaved changes
            if (_definitionEditor != null && _definitionEditor.HasUnsavedChanges)
            {
                var result = WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgUnsavedChanges"),
                    "Warning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _definitionEditor.SaveCommand.Execute(null);
                }
            }

            // Check for active recording
            if (App.RecordingService.IsRecording)
            {
                var result = WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgConfirmStop"),
                    "Warning",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        App.RecordingService.StopRecording();
                    }
                    catch
                    {
                        // Ignore errors during shutdown
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void SetLocalizedStrings()
        {
            Title = ResourceHelper.GetString("AppName");
            MenuFile.Header = ResourceHelper.GetString("MenuFile");
            MenuFileNew.Header = ResourceHelper.GetString("MenuFileNew");
            MenuFileOpen.Header = ResourceHelper.GetString("MenuFileOpen");
            MenuFileSave.Header = ResourceHelper.GetString("MenuFileSave");
            MenuFileSaveAs.Header = ResourceHelper.GetString("MenuFileSaveAs");
            MenuFileExportCsv.Header = ResourceHelper.GetString("MenuFileExportCsv");
            MenuFileExit.Header = ResourceHelper.GetString("MenuFileExit");
            MenuTools.Header = ResourceHelper.GetString("MenuTools");
            MenuToolsSettings.Header = ResourceHelper.GetString("MenuToolsSettings");
            MenuHelp.Header = ResourceHelper.GetString("MenuHelp");
            MenuHelpAbout.Header = ResourceHelper.GetString("MenuHelpAbout");

            ToolbarNewDefinition.ToolTip = ResourceHelper.GetString("MenuFileNew");
            ToolbarOpenDefinition.ToolTip = ResourceHelper.GetString("MenuFileOpen");
            ToolbarSaveDefinition.ToolTip = ResourceHelper.GetString("MenuFileSave");
            ToolbarStartRecording.ToolTip = ResourceHelper.GetString("BtnStartRecording");
            ToolbarStopRecording.ToolTip = ResourceHelper.GetString("BtnStopRecording");
            ToolbarExportCsv.ToolTip = ResourceHelper.GetString("MenuFileExportCsv");

            WelcomeTitleTextBlock.Text = ResourceHelper.GetString("WelcomeTitle");
            WelcomeSubtitleTextBlock.Text = ResourceHelper.GetString("WelcomeSubtitle");
            WelcomeNewDefinitionButton.Content = ResourceHelper.GetString("WelcomeCreateNew");
            WelcomeOpenDefinitionButton.Content = ResourceHelper.GetString("WelcomeOpenExisting");
        }

        private void ShowWelcomeScreen()
        {
            MainContentControl.Content = null;
            UpdateToolbarState();
        }

        private void ShowDefinitionEditor()
        {
            _definitionEditor = new DefinitionEditorControl();
            _definitionEditor.Definition = _currentDefinition;
            MainContentControl.Content = _definitionEditor;
            UpdateToolbarState();
        }

        private void ShowRecordingView()
        {
            _recordingView = new RecordingViewControl();
            _recordingView.Definition = _currentDefinition;
            _recordingView.RecordingCompleted += RecordingView_RecordingCompleted;
            MainContentControl.Content = _recordingView;
            UpdateToolbarState();
        }

        private void RecordingView_RecordingCompleted(object? sender, TimeStudyRecording recording)
        {
            ShowResultsView(recording);
        }

        private void ShowResultsView(TimeStudyRecording recording)
        {
            _resultsView = new ResultsViewControl();
            _resultsView.Recording = recording;
            _resultsView.CloseRequested += (s, e) => ShowWelcomeScreen();
            MainContentControl.Content = _resultsView;
            UpdateToolbarState();
        }

        private void UpdateToolbarState()
        {
            ToolbarSaveDefinition.IsEnabled = _definitionEditor != null && _currentDefinition != null;
            ToolbarStartRecording.IsEnabled = _currentDefinition != null && !App.RecordingService.IsRecording;
            ToolbarStopRecording.IsEnabled = App.RecordingService.IsRecording;
            ToolbarExportCsv.IsEnabled = _resultsView != null;
        }

        // File Menu Handlers

        private void NewDefinition_Click(object sender, RoutedEventArgs e)
        {
            _currentDefinition = App.DefinitionService.CreateDefinition();
            ShowDefinitionEditor();
        }

        private void OpenDefinition_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SelectDefinitionDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                _currentDefinition = dialog.SelectedDefinition;
                ShowDefinitionEditor();
            }
        }

        private void SaveDefinition_Click(object sender, RoutedEventArgs e)
        {
            if (_definitionEditor != null)
            {
                _definitionEditor.SaveCommand.Execute(null);
            }
        }

        private void SaveDefinitionAs_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDefinition == null) return;

            var copy = App.DefinitionService.CreateCopy(_currentDefinition);
            _currentDefinition = copy;
            ShowDefinitionEditor();
        }

        private void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            if (_resultsView != null)
            {
                // This will be handled by the ResultsViewControl's export functionality
                // or we can show an error message if no recording is loaded
            }
            else
            {
                WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgNoRecording"),
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Toolbar Handlers

        private void StartRecording_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDefinition == null)
            {
                WpfMessageBox.Show(
                    ResourceHelper.GetString("MsgSelectDefinition"),
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            ShowRecordingView();
        }

        private void StopRecording_Click(object sender, RoutedEventArgs e)
        {
            if (_recordingView != null)
            {
                // Simulate clicking the stop button
                // The RecordingViewControl will handle this
            }
        }

        // Tools Menu Handlers

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SettingsDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        // Help Menu Handlers

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AboutDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
    }
}