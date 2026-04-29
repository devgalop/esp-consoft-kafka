using Microsoft.AspNetCore.Routing;

namespace devgalop.lrn.kafka.Features;

public interface IFeatureModule
{
    void RegisterDependencies(IServiceCollection services);
    void MapEndpoints(IEndpointRouteBuilder app);
}