using devgalop.lrn.kafka.Features.Logging.Contracts;
using devgalop.lrn.kafka.Features.Logging.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace devgalop.lrn.kafka.Infrastructure.Persistence;

public sealed class LogRepository : ILogRepository
{
    private readonly LoggingDbContext _context;
    private readonly ILogger<LogRepository> _logger;

    public LogRepository(LoggingDbContext context, ILogger<LogRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(string source, string message, CancellationToken cancellationToken = default)
    {
        var logEntry = new LogEntry
        {
            UnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Source = source,
            Message = message
        };

        _context.Logs.Add(logEntry);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Log entry saved: Source={Source}, UnixTime={UnixTime}, Message={Message}", source, logEntry.UnixTime, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save log entry: Source={Source}", source);
        }
    }
}