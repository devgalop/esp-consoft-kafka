using devgalop.lrn.kafka.Features.Notifications.Contracts;
using devgalop.lrn.kafka.Features.Notifications.Endpoints;
using devgalop.lrn.kafka.Features.Notifications.Models;
using devgalop.lrn.kafka.Shared.Mediator;

namespace devgalop.lrn.kafka.Features.Notifications.Handlers;

public class NotificationHandler(IPublisher publisher) : ICommandHandler<NotificationRequest>
{
    public async Task HandleAsync(NotificationRequest request)
    {
        var message = new NotificationMessage(request.Title, request.Author, request.Content);
        await publisher.PublishAsync(message);
    }
}