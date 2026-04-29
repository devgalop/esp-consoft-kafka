using devgalop.lrn.kafka.Features.Notifications.Endpoints;
using devgalop.lrn.kafka.Features.Notifications.Handlers;
using devgalop.lrn.kafka.Shared.Endpoint;
using Microsoft.AspNetCore.Routing;

namespace devgalop.lrn.kafka.Features.Notifications;

public class NotificationsFeature : IFeatureModule
{
    public void RegisterDependencies(IServiceCollection services)
    {
        services.AddTransient<IEndpoint, NotificationEndpoint>();
        services.AddSingleton<Shared.Mediator.ICommandHandler<NotificationRequest>, NotificationHandler>();
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var endpoint = app.ServiceProvider.GetServices<IEndpoint>()
            .OfType<NotificationEndpoint>()
            .FirstOrDefault();
        endpoint?.MapEndpoint(app);
    }
}