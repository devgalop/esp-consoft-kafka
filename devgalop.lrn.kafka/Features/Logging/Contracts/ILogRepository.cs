namespace devgalop.lrn.kafka.Features.Logging.Contracts;

public interface ILogRepository
{
    /// <summary>
    /// Agrega un nuevo mensaje de log a la base de datos.
    /// </summary>
    /// <param name="source">La fuente del mensaje de log.</param>
    /// <param name="message">El contenido del mensaje de log.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Tarea que representa la operación asincrónica.</returns>
    Task AddAsync(string source, string message, CancellationToken cancellationToken = default);
}