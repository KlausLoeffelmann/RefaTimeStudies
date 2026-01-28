using System.Globalization;
using System.Text;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for exporting time study recordings to CSV format.
/// </summary>
public sealed class CsvExportService
{
    private readonly SettingsService _settingsService;

    public CsvExportService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Exports a recording to CSV format.
    /// </summary>
    public async Task ExportAsync(
        TimeStudyRecording recording,
        string filePath,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(recording);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        string content = GenerateCsv(recording);
        await File.WriteAllTextAsync(filePath, content, Encoding.UTF8, cancellationToken);
    }

    /// <summary>
    /// Generates CSV content for a recording.
    /// </summary>
    public string GenerateCsv(TimeStudyRecording recording)
    {
        ArgumentNullException.ThrowIfNull(recording);

        string delimiter = _settingsService.Settings.CsvDelimiter;
        StringBuilder sb = new();

        // Header section
        AppendLine(sb, delimiter,
            Quote(Resources.CsvHeaderDefinition),
            Quote(Resources.CsvHeaderRecordingId),
            Quote(Resources.CsvHeaderStartedAt),
            Quote(Resources.CsvHeaderCompletedAt));

        AppendLine(sb, delimiter,
            Quote(recording.DefinitionName),
            Quote(recording.Id.ToString()),
            Quote(recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss")),
            Quote(recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""));

        sb.AppendLine();

        // Detail section header
        AppendLine(sb, delimiter,
            Quote(Resources.CsvHeaderSeq),
            Quote(Resources.LblOrderNumber),
            Quote(Resources.LblDescription),
            Quote(Resources.CsvHeaderTimestamp),
            Quote(Resources.LblProgressiveTime),
            Quote(Resources.CsvHeaderDurationSec),
            Quote(Resources.CsvHeaderDurationMin),
            Quote(Resources.CsvHeaderDimensionValue));

        // Detail entries
        foreach (TimeEntry entry in recording.Entries)
        {
            AppendLine(sb, delimiter,
                entry.SequenceNumber.ToString(),
                entry.ProcessStepOrderNumber.ToString(),
                Quote(entry.ProcessStepDescription),
                Quote(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")),
                Quote(FormatTimeSpan(entry.ElapsedFromStart)),
                entry.Duration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                (entry.Duration.TotalMinutes).ToString("F2", CultureInfo.InvariantCulture),
                entry.DimensionValue?.ToString(CultureInfo.InvariantCulture) ?? "");
        }

        sb.AppendLine();

        // Summary section
        sb.AppendLine(Quote(Resources.CsvHeaderSummary));

        AppendLine(sb, delimiter,
            Quote(Resources.LblOrderNumber),
            Quote(Resources.LblDescription),
            Quote(Resources.CsvHeaderCount),
            Quote(Resources.CsvHeaderTotalDurationSec),
            Quote(Resources.CsvHeaderTotalDurationMin),
            Quote(Resources.CsvHeaderAvgDurationSec));

        foreach (ProcessStepSummary summary in recording.GetSummary())
        {
            AppendLine(sb, delimiter,
                summary.OrderNumber.ToString(),
                Quote(summary.Description),
                summary.Count.ToString(),
                summary.TotalDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                summary.TotalDuration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture),
                summary.AverageDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture));
        }

        return sb.ToString();
    }

    private static void AppendLine(StringBuilder sb, string delimiter, params string[] values)
    {
        sb.AppendLine(string.Join(delimiter, values));
    }

    private static string Quote(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "\"\"";
        }

        // Escape quotes by doubling them
        string escaped = value.Replace("\"", "\"\"");
        return $"\"{escaped}\"";
    }

    private static string FormatTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan.TotalHours >= 1)
        {
            return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
}
