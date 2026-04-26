using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Producer;

public record NotificationRequest(string Title, string Author, string Content): ICommand
{
    public string Serialize() => System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
        WriteIndented = false
    });
}

public class ProducerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async(NotificationRequest request, IMediator mediator) =>
        {
            try
            {
                Console.WriteLine($"Recibe petición: {request.Serialize()}");
                await mediator.SendAsync(request);
                return Results.Ok("Petición recibida y procesada correctamente");    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
                return Results.Problem($"Ocurrió un error al procesar la petición, por favor intente nuevamente. Mensaje de error: {ex.Message}", statusCode: 400);
            }
            
        })
        .WithName("ProduceMessage")
        .WithTags("Producer")
        .WithDescription("Endpoint para producir mensajes a Kafka")
        .Accepts<NotificationRequest>("application/json")
        .Produces(200)
        .Produces(400);

    }
}
