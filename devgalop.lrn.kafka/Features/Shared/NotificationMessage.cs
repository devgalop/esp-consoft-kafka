using System.Text.Json;
using devgalop.lrn.kafka.Shared.Messages;

namespace devgalop.lrn.kafka.Features.Shared;

/// <summary>
/// Representa una notificación con título, autor y contenido, junto con un identificador único y una marca de tiempo en formato UTC. Implementa la interfaz IMessage para permitir su serialización a un formato de cadena adecuado para la transmisión.
/// </summary>
/// <param name="Title"></param>
/// <param name="Author"></param>
/// <param name="Content"></param>
public record NotificationMessage(
    string Title,
    string Author,
    string Content
) : IMessage
{   
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string UtcTimestamp { get; private set; } = DateTime.UtcNow.ToString("o");
    public string Serialize()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });
    }

    public string GetIdentifier()
    {
        return Id.ToString();
    }
}
