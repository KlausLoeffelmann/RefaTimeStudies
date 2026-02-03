using System.IO;
using System.Text;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Exports time study recordings to CSV format.
/// </summary>
public class CsvExportService : ICsvExportService
{
    /// <summary>
    /// Exports a recording to CSV format with detail and summary sections.
    /// </summary>
    public async Task ExportAsync(TimeStudyRecording recording, string filePath, char delimiter = ';')
    {
        var sb = new StringBuilder();
        var d = delimiter;

        // Header section
        sb.AppendLine($"\"Definition\"{d}\"Recording ID\"{d}\"Started At\"{d}\"Completed At\"");
        sb.AppendLine($"\"{Escape(recording.DefinitionName)}\"{d}\"{recording.Id}\"{d}\"{recording.StartedAt:yyyy-MM-dd HH:mm:ss}\"{d}\"{recording.CompletedAt:yyyy-MM-dd HH:mm:ss}\"");
        sb.AppendLine();

        // Detail section
        sb.AppendLine($"\"Seq\"{d}\"Order#\"{d}\"Description\"{d}\"Timestamp\"{d}\"Progressive Time\"{d}\"Duration (s)\"{d}\"Duration (min)\"{d}\"Dimension Value\"");

        foreach (var entry in recording.Entries)
        {
            sb.AppendLine(string.Join(d.ToString(), new[]
            {
                entry.SequenceNumber.ToString(),
                entry.ProcessStepOrderNumber.ToString(),
                $"\"{Escape(entry.ProcessStepDescription)}\"",
                $"\"{entry.Timestamp:yyyy-MM-dd HH:mm:ss}\"",
                $"\"{entry.ElapsedFromStart:hh\\:mm\\:ss}\"",
                entry.Duration.TotalSeconds.ToString("F2"),
                entry.Duration.TotalMinutes.ToString("F2"),
                entry.DimensionValue?.ToString() ?? ""
            }));
        }

        sb.AppendLine();

        // Summary section
        sb.AppendLine("\"SUMMARY\"");
        sb.AppendLine($"\"Order#\"{d}\"Description\"{d}\"Count\"{d}\"Total Duration (s)\"{d}\"Total Duration (min)\"{d}\"Avg Duration (s)\"");

        var summary = recording.Entries
            .GroupBy(e => e.ProcessStepOrderNumber)
            .Select(g => new
            {
                OrderNumber = g.Key,
                Description = g.First().ProcessStepDescription,
                Count = g.Count(),
                TotalSeconds = g.Sum(e => e.Duration.TotalSeconds),
                AvgSeconds = g.Average(e => e.Duration.TotalSeconds)
            })
            .OrderBy(s => s.OrderNumber);

        foreach (var item in summary)
        {
            sb.AppendLine(string.Join(d.ToString(), new[]
            {
                item.OrderNumber.ToString(),
                $"\"{Escape(item.Description)}\"",
                item.Count.ToString(),
                item.TotalSeconds.ToString("F2"),
                (item.TotalSeconds / 60).ToString("F2"),
                item.AvgSeconds.ToString("F2")
            }));
        }

        // Write with UTF-8 BOM for Excel compatibility
        await File.WriteAllTextAsync(filePath, sb.ToString(), new UTF8Encoding(true));
    }

    private static string Escape(string value)
    {
        return value.Replace("\"", "\"\"");
    }
}
