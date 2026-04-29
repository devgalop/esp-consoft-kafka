using System.Text.Json;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Shared.Base;

public abstract record RequestDto : ICommand
{
    public string Serialize() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    });
}