using devgalop.lrn.kafka.Shared.Base;
using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;
using Microsoft.Extensions.Logging;

namespace devgalop.lrn.kafka.Features.Consumer.Endpoints;

public record ConsumeMessageRequest(int NumberOfMessages) : RequestDto;

public class ConsumeEndpoint(ILogger<ConsumeEndpoint> logger) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/consume", async (int numberOfMessages, IMediator mediator) =>
        {
            logger.LogInformation("Received request to consume {Count} messages", numberOfMessages);
            if(numberOfMessages <= 0) numberOfMessages = 1;
            await mediator.SendAsync(new ConsumeMessageRequest(numberOfMessages));
            return Results.Ok($"Finaliza procesamiento de los {numberOfMessages} mensajes solicitados");
        })
        .WithName("ConsumeMessage")
        .WithTags("Consumer");
    }
}