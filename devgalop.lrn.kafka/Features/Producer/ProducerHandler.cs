using devgalop.lrn.kafka.Features.Shared;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Producer;

public class ProducerHandler(
    IPublisher publisher
): ICommandHandler<NotificationRequest>
{
    public async Task HandleAsync(NotificationRequest request)
    {
        NotificationMessage message = new(request.Title, request.Author, request.Content);
        await publisher.PublishAsync(message);
    }
}

public static class ProducerHandlerExtensions
{
    /// <summary>
    /// Agrega el handler del productor a la aplicación.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con el handler del productor agregado.</returns>
    public static WebApplicationBuilder AddProducerHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandHandler<NotificationRequest>, ProducerHandler>();
        return builder;
    }
}
