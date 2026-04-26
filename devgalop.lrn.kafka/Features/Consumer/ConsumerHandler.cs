
using devgalop.lrn.kafka.Features.Shared;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Consumer;

public class ConsumerHandler(
    IConsumer consumer
) : ICommandHandler<ConsumeMessageRequest>
{
    public async Task HandleAsync(ConsumeMessageRequest request)
    {
        await consumer.ConsumeAsync(request.NumberOfMessages);
    }
}

public static class ConsumerHandlerExtensions
{
    /// <summary>
    /// Agrega el consumidor del topic al constructor de la aplicación web.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con el consumidor agregado.</returns>
    public static WebApplicationBuilder AddConsumerHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandHandler<ConsumeMessageRequest>, ConsumerHandler>();
        return builder;
    }
}
