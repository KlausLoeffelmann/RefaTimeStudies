using System.Globalization;
using System.Text;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for exporting time study recordings to CSV format.
/// </summary>
public class CsvExportService
{
    private readonly SettingsService _settingsService;

    public CsvExportService(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);
        _settingsService = settingsService;
    }

    /// <summary>
    /// Gets the CSV delimiter from settings.
    /// </summary>
    private string Delimiter => _settingsService.Settings.CsvDelimiter;

    /// <summary>
    /// Exports a recording to CSV format.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <param name="filePath">The file path to write to.</param>
    public void Export(TimeStudyRecording recording, string filePath)
    {
        ArgumentNullException.ThrowIfNull(recording);

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path is required.", nameof(filePath));
        }

        StringBuilder sb = new();

        // Header section
        AppendLine(sb, "Definition", "Recording ID", "Started At", "Completed At");
        AppendLine(sb,
            EscapeCsvValue(recording.DefinitionName),
            recording.Id.ToString(),
            recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? "");

        sb.AppendLine();

        // Detail header
        AppendLine(sb, "Seq", "Order#", "Description", "Timestamp", "Progressive Time",
            "Duration (s)", "Duration (min)", "Dimension Value");

        // Detail rows
        foreach (TimeEntry entry in recording.Entries)
        {
            AppendLine(sb,
                entry.SequenceNumber.ToString(CultureInfo.InvariantCulture),
                entry.ProcessStepOrderNumber.ToString(CultureInfo.InvariantCulture),
                EscapeCsvValue(entry.ProcessStepDescription),
                entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                FormatTimeSpan(entry.ElapsedFromStart),
                entry.Duration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                (entry.Duration.TotalMinutes).ToString("F2", CultureInfo.InvariantCulture),
                entry.DimensionValue?.ToString(CultureInfo.InvariantCulture) ?? "");
        }

        sb.AppendLine();

        // Summary section
        sb.AppendLine("SUMMARY");
        AppendLine(sb, "Order#", "Description", "Count", "Total Duration (s)",
            "Total Duration (min)", "Avg Duration (s)");

        foreach (ProcessStepSummary summary in recording.GetSummary())
        {
            AppendLine(sb,
                summary.OrderNumber.ToString(CultureInfo.InvariantCulture),
                EscapeCsvValue(summary.Description),
                summary.EntryCount.ToString(CultureInfo.InvariantCulture),
                summary.TotalDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                summary.TotalDuration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture),
                summary.AverageDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture));
        }

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    /// <summary>
    /// Exports a recording to CSV and returns the content as a string.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <returns>The CSV content.</returns>
    public string ExportToString(TimeStudyRecording recording)
    {
        string tempPath = Path.GetTempFileName();

        try
        {
            Export(recording, tempPath);
            return File.ReadAllText(tempPath, Encoding.UTF8);
        }
        finally
        {
            try
            {
                File.Delete(tempPath);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }

    /// <summary>
    /// Appends a CSV line with the specified values.
    /// </summary>
    private void AppendLine(StringBuilder sb, params string[] values)
    {
        sb.AppendLine(string.Join(Delimiter, values.Select(EscapeCsvValue)));
    }

    /// <summary>
    /// Escapes a value for CSV format.
    /// </summary>
    private string EscapeCsvValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "\"\"";
        }

        // If the value contains the delimiter, quotes, or newlines, wrap in quotes
        if (value.Contains(Delimiter, StringComparison.Ordinal) ||
            value.Contains('"') ||
            value.Contains('\n') ||
            value.Contains('\r'))
        {
            // Escape quotes by doubling them
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return $"\"{value}\"";
    }

    /// <summary>
    /// Formats a TimeSpan as HH:mm:ss.
    /// </summary>
    private static string FormatTimeSpan(TimeSpan ts)
    {
        return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}";
    }
}
