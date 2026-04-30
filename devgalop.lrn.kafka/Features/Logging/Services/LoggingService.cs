using devgalop.lrn.kafka.Features.Logging.Contracts;
using devgalop.lrn.kafka.Features.Logging.Models;

namespace devgalop.lrn.kafka.Features.Logging.Services;

public sealed class LoggingService(
    ILogRepository logRepository
) : ILogWriter
{
    public async Task WriteAsync(string source, string message, CancellationToken cancellationToken = default)
    {
        await logRepository.AddAsync(source, message, cancellationToken);
    }
}