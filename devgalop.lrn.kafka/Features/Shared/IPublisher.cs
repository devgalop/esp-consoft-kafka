using devgalop.lrn.kafka.Shared.Messages;

namespace devgalop.lrn.kafka.Features.Shared;

public interface IPublisher
{
    /// <summary>
    /// Publica un mensaje de forma asíncrona a Kafka.
    /// </summary>
    /// <param name="message">El mensaje que se va a publicar.</param>
    /// <returns>Una tarea que representa la operación de publicación asíncrona.</returns>
    Task PublishAsync(IMessage message);
}
