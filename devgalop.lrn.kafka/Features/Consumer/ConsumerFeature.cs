using devgalop.lrn.kafka.Features.Consumer.Endpoints;
using devgalop.lrn.kafka.Features.Consumer.Handlers;
using devgalop.lrn.kafka.Shared.Endpoint;
using Microsoft.AspNetCore.Routing;

namespace devgalop.lrn.kafka.Features.Consumer;

public class ConsumerFeature : IFeatureModule
{
    public void RegisterDependencies(IServiceCollection services)
    {
        services.AddTransient<IEndpoint, ConsumeEndpoint>();
        services.AddSingleton<Shared.Mediator.ICommandHandler<ConsumeMessageRequest>, ConsumeHandler>();
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var endpoint = app.ServiceProvider.GetServices<IEndpoint>()
            .OfType<ConsumeEndpoint>()
            .FirstOrDefault();
        endpoint?.MapEndpoint(app);
    }
}