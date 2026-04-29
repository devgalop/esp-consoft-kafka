using devgalop.lrn.kafka.Features.Notifications.Contracts;

namespace devgalop.lrn.kafka.Features.Notifications.Contracts;

public interface IPublisher
{
    Task PublishAsync(IMessage message);
}