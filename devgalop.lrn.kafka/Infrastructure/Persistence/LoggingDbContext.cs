using devgalop.lrn.kafka.Features.Logging.Models;
using devgalop.lrn.kafka.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace devgalop.lrn.kafka.Infrastructure.Persistence;

public sealed class LoggingDbContext(DbContextOptions<LoggingDbContext> options) : DbContext(options)
{

    public DbSet<LogEntry> Logs => Set<LogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntry>(entity =>
        {
            entity.ToTable(LoggingDbConstants.TableName);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName(LoggingDbConstants.IdColumnName)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UnixTime)
                .HasColumnName(LoggingDbConstants.UnixTimeColumnName)
                .IsRequired();
            entity.Property(e => e.Source)
                .HasColumnName(LoggingDbConstants.SourceColumnName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Message)
                .HasColumnName(LoggingDbConstants.MessageColumnName)
                .IsRequired()
                .HasMaxLength(2000);
        });
    }
}