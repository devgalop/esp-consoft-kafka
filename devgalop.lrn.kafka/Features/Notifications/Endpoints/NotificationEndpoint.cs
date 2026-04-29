using devgalop.lrn.kafka.Shared.Base;
using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;
using Microsoft.Extensions.Logging;

namespace devgalop.lrn.kafka.Features.Notifications.Endpoints;

public record NotificationRequest(string Title, string Author, string Content) : RequestDto;

public class NotificationEndpoint(ILogger<NotificationEndpoint> logger) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (NotificationRequest request, IMediator mediator) =>
        {
            try
            {
                logger.LogInformation("Received request: {Request}", request.Serialize());
                await mediator.SendAsync(request);
                return Results.Ok("Petición recibida y procesada correctamente");    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing request");
                return Results.Problem($"Ocurrió un error al procesar la petición, por favor intente nuevamente. Mensaje de error: {ex.Message}", statusCode: 400);
            }
        })
        .WithName("ProduceMessage")
        .WithTags("Notifications")
        .WithDescription("Endpoint para producir mensajes a Kafka")
        .Accepts<NotificationRequest>("application/json")
        .Produces(200)
        .Produces(400);
    }
}