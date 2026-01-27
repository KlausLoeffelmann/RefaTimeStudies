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
        _settingsService = settingsService;
    }

    /// <summary>
    /// Exports a recording to CSV format.
    /// </summary>
    public string Export(TimeStudyRecording recording, TimeStudyDefinition definition)
    {
        var delimiter = _settingsService.CurrentSettings.CsvDelimiter;
        var sb = new StringBuilder();

        // Header section
        sb.AppendLine(FormatRow(delimiter, "Definition", "Recording ID", "Started At", "Completed At"));
        sb.AppendLine(FormatRow(delimiter,
            recording.DefinitionName,
            recording.Id.ToString(),
            recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""));
        sb.AppendLine();

        // Detail section header
        sb.AppendLine(FormatRow(delimiter,
            "Seq", "Order#", "Description", "Timestamp",
            "Progressive Time", "Duration (s)", "Duration (min)", "Dimension Value"));

        // Detail rows
        foreach (var entry in recording.Entries)
        {
            sb.AppendLine(FormatRow(delimiter,
                entry.SequenceNumber.ToString(),
                entry.ProcessStepOrderNumber.ToString(),
                entry.ProcessStepDescription,
                entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                entry.ElapsedFromStart.ToString(@"hh\:mm\:ss"),
                entry.Duration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                (entry.Duration.TotalSeconds / 60.0).ToString("F2", CultureInfo.InvariantCulture),
                entry.DimensionValue?.ToString(CultureInfo.InvariantCulture) ?? ""));
        }
        sb.AppendLine();

        // Summary section
        sb.AppendLine(FormatRow(delimiter, "SUMMARY"));
        sb.AppendLine(FormatRow(delimiter,
            "Order#", "Description", "Count",
            "Total Duration (s)", "Total Duration (min)", "Avg Duration (s)"));

        var summaryData = GetSummaryData(recording, definition);
        foreach (var item in summaryData.OrderBy(s => s.OrderNumber))
        {
            sb.AppendLine(FormatRow(delimiter,
                item.OrderNumber.ToString(),
                item.Description,
                item.Count.ToString(),
                item.TotalDurationSeconds.ToString("F2", CultureInfo.InvariantCulture),
                item.TotalDurationMinutes.ToString("F2", CultureInfo.InvariantCulture),
                item.AverageDurationSeconds.ToString("F2", CultureInfo.InvariantCulture)));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Exports a recording to a file.
    /// </summary>
    public void ExportToFile(TimeStudyRecording recording, TimeStudyDefinition definition, string filePath)
    {
        var csv = Export(recording, definition);
        File.WriteAllText(filePath, csv, Encoding.UTF8);
    }

    /// <summary>
    /// Gets the suggested file name for a CSV export.
    /// </summary>
    public string GetSuggestedFileName(TimeStudyRecording recording)
    {
        var safeName = string.Join("_", recording.DefinitionName.Split(Path.GetInvalidFileNameChars()));
        var dateStr = recording.StartedAt.ToString("yyyy-MM-dd_HHmmss");
        return $"{safeName}_{dateStr}.csv";
    }

    private static string FormatRow(string delimiter, params string[] values)
    {
        var escapedValues = values.Select(v => EscapeCsvValue(v, delimiter));
        return string.Join(delimiter, escapedValues);
    }

    private static string EscapeCsvValue(string value, string delimiter)
    {
        if (string.IsNullOrEmpty(value))
            return "\"\"";

        // Always quote values for consistency and to handle special characters
        var escaped = value.Replace("\"", "\"\"");
        return $"\"{escaped}\"";
    }

    private List<SummaryItem> GetSummaryData(TimeStudyRecording recording, TimeStudyDefinition definition)
    {
        var items = new List<SummaryItem>();

        var grouped = recording.Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .ToList();

        foreach (var group in grouped)
        {
            var step = definition.ProcessSteps.FirstOrDefault(s => s.OrderNumber == group.Key);
            var totalDuration = TimeSpan.FromTicks(group.Sum(e => e.Duration.Ticks));
            var count = group.Count();

            items.Add(new SummaryItem
            {
                OrderNumber = group.Key,
                Description = step?.Description ?? group.First().ProcessStepDescription,
                Count = count,
                TotalDurationSeconds = totalDuration.TotalSeconds,
                TotalDurationMinutes = totalDuration.TotalMinutes,
                AverageDurationSeconds = count > 0 ? totalDuration.TotalSeconds / count : 0
            });
        }

        return items;
    }

    private class SummaryItem
    {
        public int OrderNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; }
        public double TotalDurationSeconds { get; set; }
        public double TotalDurationMinutes { get; set; }
        public double AverageDurationSeconds { get; set; }
    }
}
