using devgalop.lrn.kafka.Features.Logging.Models;

namespace devgalop.lrn.kafka.Features.Logging.Contracts;

public interface ILogWriter
{
    /// <summary>
    /// Escribe un mensaje de log en la base de datos.
    /// </summary>
    /// <param name="source">La fuente del mensaje de log.</param>
    /// <param name="message">El contenido del mensaje de log.</param>
    /// <param name="cancellationToken">Token de cancelación para la operación asincrónica.</param>
    /// <returns>Tarea que representa la operación asincrónica.</returns>
    Task WriteAsync(string source, string message, CancellationToken cancellationToken = default);
}
