using System.Globalization;
using System.Text;
using System.IO;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Service for exporting time study recordings to CSV format.
/// Dienst für den Export von Zeitaufnahmen im CSV-Format.
/// </summary>
public class CsvExportService
{
    private readonly string _delimiter;

    /// <summary>
    /// Initializes a new instance of CsvExportService.
    /// Initialisiert eine neue Instanz von CsvExportService.
    /// </summary>
    /// <param name="useSemicolonDelimiter">If true, uses semicolon as delimiter (German Excel); otherwise, uses comma.</param>
    public CsvExportService(bool useSemicolonDelimiter = true)
    {
        _delimiter = useSemicolonDelimiter ? ";" : ",";
    }

    /// <summary>
    /// Exports a time study recording to a CSV file.
    /// Exportiert eine Zeitaufnahme in eine CSV-Datei.
    /// </summary>
    /// <param name="recording">The recording to export.</param>
    /// <param name="filePath">The file path to save to.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous export operation.</returns>
    public async Task ExportToCsvAsync(TimeStudyRecording recording, string filePath, CancellationToken cancellationToken = default)
    {
        string content = GenerateCsvContent(recording);
        await File.WriteAllTextAsync(filePath, content, Encoding.UTF8, cancellationToken);
    }

    /// <summary>
    /// Generates the CSV content for a time study recording.
    /// Generiert den CSV-Inhalt für eine Zeitaufnahme.
    /// </summary>
    /// <param name="recording">The recording to generate CSV for.</param>
    /// <returns>The complete CSV content as a string.</returns>
    public string GenerateCsvContent(TimeStudyRecording recording)
    {
        var sb = new StringBuilder();

        sb.AppendLine(GenerateHeaderSection(recording));
        sb.AppendLine();
        sb.AppendLine(GenerateDetailSection(recording));
        sb.AppendLine();
        sb.AppendLine(GenerateSummarySection(recording));

        return sb.ToString();
    }

    /// <summary>
    /// Generates the header section of the CSV file.
    /// Generiert den Kopfbereich der CSV-Datei.
    /// </summary>
    /// <param name="recording">The recording.</param>
    /// <returns>The header section as a string.</returns>
    private string GenerateHeaderSection(TimeStudyRecording recording)
    {
        var sb = new StringBuilder();

        sb.AppendLine(EscapeField("Definition") + _delimiter + EscapeField("Recording ID") + _delimiter + EscapeField("Started At") + _delimiter + EscapeField("Completed At"));
        sb.AppendLine(
            EscapeField(recording.DefinitionName) + _delimiter +
            EscapeField(recording.Id.ToString()) + _delimiter +
            EscapeField(recording.StartedAt.ToString("yyyy-MM-dd HH:mm:ss")) + _delimiter +
            EscapeField(recording.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""));

        return sb.ToString();
    }

    /// <summary>
    /// Generates the detail section of the CSV file with all time entries.
    /// Generiert den Detailbereich der CSV-Datei mit allen Zeiteinträgen.
    /// </summary>
    /// <param name="recording">The recording.</param>
    /// <returns>The detail section as a string.</returns>
    private string GenerateDetailSection(TimeStudyRecording recording)
    {
        var sb = new StringBuilder();

        sb.AppendLine(
            EscapeField("Seq") + _delimiter +
            EscapeField("Order#") + _delimiter +
            EscapeField("Description") + _delimiter +
            EscapeField("Timestamp") + _delimiter +
            EscapeField("Progressive Time") + _delimiter +
            EscapeField("Duration (s)") + _delimiter +
            EscapeField("Duration (min)") + _delimiter +
            EscapeField("Dimension Value"));

        foreach (var entry in recording.Entries)
        {
            sb.AppendLine(
                EscapeField(entry.SequenceNumber.ToString()) + _delimiter +
                EscapeField(entry.ProcessStepOrderNumber.ToString()) + _delimiter +
                EscapeField(entry.ProcessStepDescription) + _delimiter +
                EscapeField(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")) + _delimiter +
                EscapeField(entry.ElapsedFromStart.ToString(@"hh\:mm\:ss")) + _delimiter +
                EscapeField(entry.Duration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture)) + _delimiter +
                EscapeField(entry.Duration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture)) + _delimiter +
                EscapeField(entry.DimensionValue?.ToString(CultureInfo.InvariantCulture) ?? ""));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Generates the summary section of the CSV file with per-step aggregates.
    /// Generiert den Zusammenfassungsbereich der CSV-Datei mit Aggregaten pro Schritt.
    /// </summary>
    /// <param name="recording">The recording.</param>
    /// <returns>The summary section as a string.</returns>
    private string GenerateSummarySection(TimeStudyRecording recording)
    {
        var sb = new StringBuilder();

        var summaries = recording.Entries
            .GroupBy(e => new { e.ProcessStepOrderNumber, e.ProcessStepDescription })
            .Select(g => new
            {
                OrderNumber = g.Key.ProcessStepOrderNumber,
                Description = g.Key.ProcessStepDescription,
                Count = g.Count(),
                TotalDuration = TimeSpan.FromSeconds(g.Sum(e => e.Duration.TotalSeconds))
            })
            .OrderBy(s => s.OrderNumber)
            .ToList();

        sb.AppendLine(EscapeField("SUMMARY"));
        sb.AppendLine(
            EscapeField("Order#") + _delimiter +
            EscapeField("Description") + _delimiter +
            EscapeField("Count") + _delimiter +
            EscapeField("Total Duration (s)") + _delimiter +
            EscapeField("Total Duration (min)") + _delimiter +
            EscapeField("Avg Duration (s)"));

        foreach (var summary in summaries)
        {
            double avgDuration = summary.Count > 0 ? summary.TotalDuration.TotalSeconds / summary.Count : 0;

            sb.AppendLine(
                EscapeField(summary.OrderNumber.ToString()) + _delimiter +
                EscapeField(summary.Description) + _delimiter +
                EscapeField(summary.Count.ToString()) + _delimiter +
                EscapeField(summary.TotalDuration.TotalSeconds.ToString("F2", CultureInfo.InvariantCulture)) + _delimiter +
                EscapeField(summary.TotalDuration.TotalMinutes.ToString("F2", CultureInfo.InvariantCulture)) + _delimiter +
                EscapeField(avgDuration.ToString("F2", CultureInfo.InvariantCulture)));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Escapes a field value for CSV format.
    /// Maskiert einen Feldwert für das CSV-Format.
    /// </summary>
    /// <param name="field">The field value to escape.</param>
    /// <returns>The escaped field value.</returns>
    private string EscapeField(string field)
    {
        if (string.IsNullOrEmpty(field))
        {
            return "";
        }

        if (field.Contains(_delimiter) || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }

        return field;
    }
}