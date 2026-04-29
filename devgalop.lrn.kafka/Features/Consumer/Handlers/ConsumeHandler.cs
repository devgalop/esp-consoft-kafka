using devgalop.lrn.kafka.Features.Consumer.Contracts;
using devgalop.lrn.kafka.Features.Consumer.Endpoints;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Consumer.Handlers;

public class ConsumeHandler(IConsumer consumer) : ICommandHandler<ConsumeMessageRequest>
{
    public async Task HandleAsync(ConsumeMessageRequest request)
    {
        await consumer.ConsumeAsync(request.NumberOfMessages);
    }
}