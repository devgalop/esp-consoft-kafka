
using devgalop.lrn.kafka.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using devgalop.lrn.kafka.Features.Logging.Contracts;
using devgalop.lrn.kafka.Features.Logging.Services;

namespace devgalop.lrn.kafka.Infrastructure.Persistence.Extensions;

public static class LoggingPersistenceExtensions
{
    public static WebApplicationBuilder AddLoggingPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("LoggingDb")
            ?? throw new InvalidOperationException(
                $"Connection string '{LoggingDbConstants.ConnectionStringKey}' is not configured.");

        builder.Services.AddDbContext<LoggingDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        builder.Services.AddScoped<ILogWriter, LoggingService>();
        builder.Services.AddScoped<ILogRepository, LogRepository>();

        return builder;
    }

    public static async Task UseLoggingPersistenceAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LoggingDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<LoggingDbContext>>();

        try
        {
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Logging database initialized successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to initialize logging database.");
        }
    }
}