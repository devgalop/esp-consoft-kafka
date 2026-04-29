using System.Text.Json;
using devgalop.lrn.kafka.Features.Notifications.Contracts;

namespace devgalop.lrn.kafka.Features.Notifications.Models;

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

    public string GetIdentifier() => Id.ToString();
}