using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Consumer;

public record ConsumeMessageRequest(int NumberOfMessages): ICommand
{
    public string Serialize() => System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
        WriteIndented = false
    });
}

public class ConsumerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/consume", async (int numberOfMessages, IMediator mediator) =>
        {
            Console.WriteLine($"Recibe petición para consumir {numberOfMessages} mensajes");
            if(numberOfMessages <= 0) numberOfMessages = 1;
            await mediator.SendAsync(new ConsumeMessageRequest(numberOfMessages));
            return Results.Ok($"Finaliza procesamiento de los {numberOfMessages} mensajes solicitados");
        })
        .WithName("ConsumeMessage")
        .WithTags("Consumer");
    }
}
