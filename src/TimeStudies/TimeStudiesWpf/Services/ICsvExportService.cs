using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Services;

/// <summary>
/// Interface for CSV export functionality.
/// </summary>
public interface ICsvExportService
{
    /// <summary>
    /// Exports a recording to CSV format.
    /// </summary>
    Task ExportAsync(TimeStudyRecording recording, string filePath, char delimiter = ';');
}
