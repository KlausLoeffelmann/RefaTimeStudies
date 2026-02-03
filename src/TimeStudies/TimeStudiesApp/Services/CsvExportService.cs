using System.Globalization;
using System.Text;
using TimeStudiesApp.Models;

namespace TimeStudiesApp.Services;

/// <summary>
/// Service for exporting time study recordings to CSV format.
/// </summary>
public class CsvExportService
{
    /// <summary>
    /// Exports a recording to a CSV file.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <param name="filePath">The output file path.</param>
    /// <param name="delimiter">CSV delimiter (default semicolon for German Excel compatibility).</param>
    public void Export(TimeStudyRecording recording, string filePath, char delimiter = ';')
    {
        using var writer = new StreamWriter(filePath, false, Encoding.UTF8);

        WriteHeader(writer, recording, delimiter);
        writer.WriteLine();
        WriteDetailSection(writer, recording, delimiter);
        writer.WriteLine();
        WriteSummarySection(writer, recording, delimiter);
    }

    /// <summary>
    /// Exports a recording to a string.
    /// </summary>
    public string ExportToString(TimeStudyRecording recording, char delimiter = ';')
    {
        using var writer = new StringWriter();

        WriteHeader(writer, recording, delimiter);
        writer.WriteLine();
        WriteDetailSection(writer, recording, delimiter);
        writer.WriteLine();
        WriteSummarySection(writer, recording, delimiter);

        return writer.ToString();
    }

    private void WriteHeader(TextWriter writer, TimeStudyRecording recording, char delimiter)
    {
        // Header row
        writer.WriteLine(string.Join(delimiter.ToString(),
            Quote("Definition"),
            Quote("Recording ID"),
            Quote("Started At"),
            Quote("Completed At")));

        // Header values
        writer.WriteLine(string.Join(delimiter.ToString(),
            Quote(recording.DefinitionName),
            Quote(recording.Id.ToString()),
            Quote(recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss")),
            Quote(recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "")));
    }

    private void WriteDetailSection(TextWriter writer, TimeStudyRecording recording, char delimiter)
    {
        // Detail header
        writer.WriteLine(string.Join(delimiter.ToString(),
            Quote("Seq"),
            Quote("Order#"),
            Quote("Description"),
            Quote("Timestamp"),
            Quote("Progressive Time"),
            Quote("Duration (s)"),
            Quote("Duration (min)"),
            Quote("Dimension Value")));

        // Detail rows
        foreach (var entry in recording.Entries)
        {
            writer.WriteLine(string.Join(delimiter.ToString(),
                entry.SequenceNumber.ToString(),
                entry.ProcessStepOrderNumber.ToString(),
                Quote(entry.ProcessStepDescription),
                Quote(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")),
                Quote(FormatTimeSpan(entry.ElapsedFromStart)),
                entry.Duration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                (entry.Duration.TotalMinutes).ToString("F2", CultureInfo.InvariantCulture),
                entry.DimensionValue?.ToString(CultureInfo.InvariantCulture) ?? ""));
        }
    }

    private void WriteSummarySection(TextWriter writer, TimeStudyRecording recording, char delimiter)
    {
        writer.WriteLine(Quote("SUMMARY"));

        // Summary header
        writer.WriteLine(string.Join(delimiter.ToString(),
            Quote("Order#"),
            Quote("Description"),
            Quote("Count"),
            Quote("Total Duration (s)"),
            Quote("Total Duration (min)"),
            Quote("Avg Duration (s)")));

        // Summary rows
        foreach (var summary in recording.GetSummaryByStep())
        {
            writer.WriteLine(string.Join(delimiter.ToString(),
                summary.OrderNumber.ToString(),
                Quote(summary.Description),
                summary.Count.ToString(),
                summary.TotalDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
                summary.TotalDuration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture),
                summary.AverageDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture)));
        }

        // Total row
        var totalDuration = TimeSpan.FromTicks(recording.Entries.Sum(e => e.Duration.Ticks));
        writer.WriteLine(string.Join(delimiter.ToString(),
            Quote(""),
            Quote("TOTAL"),
            recording.Entries.Count.ToString(),
            totalDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture),
            totalDuration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture),
            Quote("")));
    }

    private static string Quote(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "\"\"";

        // Escape quotes by doubling them
        string escaped = value.Replace("\"", "\"\"");
        return $"\"{escaped}\"";
    }

    private static string FormatTimeSpan(TimeSpan ts)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            (int)ts.TotalHours,
            ts.Minutes,
            ts.Seconds);
    }
}
