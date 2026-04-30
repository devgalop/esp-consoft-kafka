namespace devgalop.lrn.kafka.Features.Logging.Models;

public sealed class LogEntry
{
    public int Id { get; init; }
    public long UnixTime { get; init; }
    public string Source { get; init; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}