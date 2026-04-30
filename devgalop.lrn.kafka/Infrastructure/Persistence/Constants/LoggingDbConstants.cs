namespace devgalop.lrn.kafka.Infrastructure.Persistence.Constants;

public static partial class LoggingDbConstants
{
    public const string TableName = "Logs";
    public const string ConnectionStringKey = "ConnectionStrings:LoggingDb";
    public const string IdColumnName = "Id";
    public const string UnixTimeColumnName = "UnixTime";
    public const string SourceColumnName = "Source";
    public const string MessageColumnName = "LogMessage";
}