using System.Text;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
///  Service for exporting time study recordings to CSV format.
/// </summary>
public class CsvExportService
{
    private readonly SettingsService _settingsService;

    /// <summary>
    ///  Initializes a new instance of the <see cref="CsvExportService"/> class.
    /// </summary>
    /// <param name="settingsService">The settings service instance.</param>
    public CsvExportService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    ///  Gets the CSV delimiter from settings.
    /// </summary>
    private string Delimiter => _settingsService.Settings.CsvDelimiter;

    /// <summary>
    ///  Exports a recording to CSV format.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <param name="filePath">The path where the CSV file should be saved.</param>
    public void Export(TimeStudyRecording recording, string filePath)
    {
        ArgumentNullException.ThrowIfNull(recording);
        ArgumentException.ThrowIfNullOrEmpty(filePath);

        StringBuilder sb = new();

        // Header section
        AppendHeaderSection(sb, recording);
        sb.AppendLine();

        // Detail section
        AppendDetailSection(sb, recording);
        sb.AppendLine();

        // Summary section
        AppendSummarySection(sb, recording);

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    /// <summary>
    ///  Exports a recording to CSV format and returns the content as a string.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <returns>The CSV content as a string.</returns>
    public string ExportToString(TimeStudyRecording recording)
    {
        ArgumentNullException.ThrowIfNull(recording);

        StringBuilder sb = new();
        AppendHeaderSection(sb, recording);
        sb.AppendLine();
        AppendDetailSection(sb, recording);
        sb.AppendLine();
        AppendSummarySection(sb, recording);

        return sb.ToString();
    }

    /// <summary>
    ///  Appends the header section to the CSV output.
    /// </summary>
    private void AppendHeaderSection(StringBuilder sb, TimeStudyRecording recording)
    {
        // Header row
        sb.AppendLine(JoinFields("Definition", "Recording ID", "Started At", "Completed At"));

        // Data row
        string startedAt = recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss");
        string completedAt = recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "";

        sb.AppendLine(JoinFields(
            EscapeField(recording.DefinitionName),
            recording.Id.ToString(),
            startedAt,
            completedAt));
    }

    /// <summary>
    ///  Appends the detail section with all time entries.
    /// </summary>
    private void AppendDetailSection(StringBuilder sb, TimeStudyRecording recording)
    {
        // Header row
        sb.AppendLine(JoinFields(
            "Seq",
            "Order#",
            "Description",
            "Timestamp",
            "Progressive Time",
            "Duration (s)",
            "Duration (min)",
            "Dimension Value"));

        // Data rows
        foreach (var entry in recording.Entries.OrderBy(e => e.SequenceNumber))
        {
            string timestamp = entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            string progressiveTime = FormatTimeSpan(entry.ElapsedFromStart);
            string durationSeconds = entry.Duration.TotalSeconds.ToString("F2");
            string durationMinutes = (entry.Duration.TotalSeconds / 60.0).ToString("F2");
            string dimensionValue = entry.DimensionValue?.ToString() ?? "";

            sb.AppendLine(JoinFields(
                entry.SequenceNumber.ToString(),
                entry.ProcessStepOrderNumber.ToString(),
                EscapeField(entry.ProcessStepDescription),
                timestamp,
                progressiveTime,
                durationSeconds,
                durationMinutes,
                dimensionValue));
        }
    }

    /// <summary>
    ///  Appends the summary section with aggregated statistics.
    /// </summary>
    private void AppendSummarySection(StringBuilder sb, TimeStudyRecording recording)
    {
        sb.AppendLine("SUMMARY");

        // Header row
        sb.AppendLine(JoinFields(
            "Order#",
            "Description",
            "Count",
            "Total Duration (s)",
            "Total Duration (min)",
            "Avg Duration (s)"));

        // Data rows
        var summary = recording.GetSummaryByProcessStep();

        foreach (var item in summary.Values.OrderBy(s => s.OrderNumber))
        {
            string totalSeconds = item.TotalDuration.TotalSeconds.ToString("F2");
            string totalMinutes = (item.TotalDuration.TotalSeconds / 60.0).ToString("F2");
            string avgSeconds = item.AverageDuration.TotalSeconds.ToString("F2");

            sb.AppendLine(JoinFields(
                item.OrderNumber.ToString(),
                EscapeField(item.Description),
                item.Count.ToString(),
                totalSeconds,
                totalMinutes,
                avgSeconds));
        }
    }

    /// <summary>
    ///  Joins fields with the configured delimiter.
    /// </summary>
    private string JoinFields(params string[] fields)
    {
        return string.Join(Delimiter, fields);
    }

    /// <summary>
    ///  Escapes a field for CSV output (handles quotes and delimiters).
    /// </summary>
    private string EscapeField(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "";
        }

        // If the value contains the delimiter, quotes, or newlines, wrap in quotes
        if (value.Contains(Delimiter) || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
        {
            // Escape existing quotes by doubling them
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }

    /// <summary>
    ///  Formats a TimeSpan as HH:MM:SS.
    /// </summary>
    private static string FormatTimeSpan(TimeSpan ts)
    {
        return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
    }
}
