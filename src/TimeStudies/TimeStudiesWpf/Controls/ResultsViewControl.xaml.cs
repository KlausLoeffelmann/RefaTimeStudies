using System.Windows;
using System.Windows.Controls;
using WpfUserControl = System.Windows.Controls.UserControl;
using WpfMessageBox = System.Windows.MessageBox;
using WpfSaveFileDialog = Microsoft.Win32.SaveFileDialog;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.Controls;

/// <summary>
/// Interaction logic for ResultsViewControl.xaml
/// </summary>
public partial class ResultsViewControl : WpfUserControl
{
    private TimeStudyRecording? _recording;
    private readonly CsvExportService _csvExportService;

    public ResultsViewControl()
    {
        InitializeComponent();
        _csvExportService = App.CsvExportService;
    }

    /// <summary>
    /// Gets or sets the recording to display.
    /// </summary>
    public TimeStudyRecording? Recording
    {
        get => _recording;
        set
        {
            _recording = value;
            LoadRecording();
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        SetLocalizedStrings();
        LoadRecording();
    }

    private void SetLocalizedStrings()
    {
        HeaderLabel.Content = ResourceHelper.GetString("ViewResults");
        RecordingInfoLabel.Content = ResourceHelper.GetString("RecordingInfo");
        DetailedEntriesLabel.Content = ResourceHelper.GetString("DetailedEntries");
        SummaryLabel.Content = ResourceHelper.GetString("Summary");
        ExportCsvButton.Content = ResourceHelper.GetString("BtnExportCsv");
        CloseButton.Content = ResourceHelper.GetString("BtnClose");

        // Set DataGrid column headers
        SequenceColumn.Header = ResourceHelper.GetString("Sequence");
        OrderNumberColumn.Header = ResourceHelper.GetString("OrderNumber");
        DescriptionColumn.Header = ResourceHelper.GetString("Description");
        TimestampColumn.Header = ResourceHelper.GetString("Timestamp");
        ProgressiveTimeColumn.Header = ResourceHelper.GetString("ProgressiveTime");
        DurationSecondsColumn.Header = ResourceHelper.GetString("Duration") + " (s)";
        DurationMinutesColumn.Header = ResourceHelper.GetString("Duration") + " (min)";
        DimensionValueColumn.Header = ResourceHelper.GetString("DimensionValue");

        SummaryOrderNumberColumn.Header = ResourceHelper.GetString("OrderNumber");
        SummaryDescriptionColumn.Header = ResourceHelper.GetString("Description");
        SummaryCountColumn.Header = ResourceHelper.GetString("SummaryCount");
        SummaryTotalDurationSecondsColumn.Header = ResourceHelper.GetString("TotalDuration") + " (s)";
        SummaryTotalDurationMinutesColumn.Header = ResourceHelper.GetString("TotalDuration") + " (min)";
        SummaryAvgDurationColumn.Header = ResourceHelper.GetString("AvgDuration");
    }

    private void LoadRecording()
    {
        if (_recording == null) return;

        // Display recording info
        DefinitionNameTextBlock.Text = $"{ResourceHelper.GetString("DefinitionName")}: {_recording.DefinitionName}";
        StartedAtTextBlock.Text = $"{ResourceHelper.GetString("StartedAt")}: {_recording.StartedAt:yyyy-MM-dd HH:mm:ss}";
        CompletedAtTextBlock.Text = $"{ResourceHelper.GetString("CompletedAt")}: {_recording.CompletedAt:yyyy-MM-dd HH:mm:ss ?? 'N/A'}";
        TotalDurationTextBlock.Text = $"{ResourceHelper.GetString("TotalDuration")}: {_recording.GetTotalDuration():hh\\:mm\\:ss}";

        // Load entries into DataGrid
        LoadEntries();
        
        // Load summary into DataGrid
        LoadSummary();
    }

    private void LoadEntries()
    {
        if (_recording == null) return;

        EntriesDataGrid.Items.Clear();

        foreach (var entry in _recording.Entries)
        {
            EntriesDataGrid.Items.Add(new
            {
                Sequence = entry.SequenceNumber,
                OrderNumber = entry.ProcessStepOrderNumber,
                Description = entry.ProcessStepDescription,
                Timestamp = entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                ProgressiveTime = entry.ElapsedFromStart.ToString(@"hh\:mm\:ss\.ff"),
                DurationSeconds = entry.Duration.TotalSeconds.ToString("F2"),
                DurationMinutes = entry.Duration.TotalMinutes.ToString("F2"),
                DimensionValue = entry.DimensionValue?.ToString() ?? ""
            });
        }
    }

    private void LoadSummary()
    {
        if (_recording == null) return;

        SummaryDataGrid.Items.Clear();

        var summaries = _recording.Entries
            .GroupBy(e => new { e.ProcessStepOrderNumber, e.ProcessStepDescription })
            .Select(g => new
            {
                OrderNumber = g.Key.ProcessStepOrderNumber,
                Description = g.Key.ProcessStepDescription,
                Count = g.Count(),
                TotalDurationSeconds = g.Sum(e => e.Duration.TotalSeconds).ToString("F2"),
                TotalDurationMinutes = g.Sum(e => e.Duration.TotalMinutes).ToString("F2"),
                AvgDurationSeconds = g.Average(e => e.Duration.TotalSeconds).ToString("F2")
            })
            .OrderBy(s => s.OrderNumber)
            .ToList();

        foreach (var summary in summaries)
        {
            SummaryDataGrid.Items.Add(new
            {
                summary.OrderNumber,
                summary.Description,
                summary.Count,
                summary.TotalDurationSeconds,
                summary.TotalDurationMinutes,
                AvgDuration = summary.AvgDurationSeconds
            });
        }
    }

    private void ExportCsv_Click(object sender, RoutedEventArgs e)
    {
        if (_recording == null) return;

        var saveFileDialog = new WpfSaveFileDialog
        {
            Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
            DefaultExt = "csv",
            FileName = $"{_recording.DefinitionName}_{_recording.StartedAt:yyyyMMdd_HHmmss}.csv",
            Title = ResourceHelper.GetString("TitleExportCsv")
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                _csvExportService.ExportToCsvAsync(_recording, saveFileDialog.FileName).Wait();
                
                WpfMessageBox.Show(
                    $"{ResourceHelper.GetString("MsgExportSuccessful")}\n{saveFileDialog.FileName}",
                    "Success",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(
                    $"Failed to export CSV: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CloseRequested;
}