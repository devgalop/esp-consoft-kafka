using devgalop.lrn.kafka.Features.Consumer;
using devgalop.lrn.kafka.Features.Notifications;

namespace devgalop.lrn.kafka.Features;

public static class FeatureModuleExtensions
{
    public static WebApplicationBuilder AddFeatureModules(this WebApplicationBuilder builder)
    {
        var modules = new IFeatureModule[] 
        { 
            new NotificationsFeature(), 
            new ConsumerFeature() 
        };
        
        foreach (var module in modules)
        {
            module.RegisterDependencies(builder.Services);
            builder.Services.AddSingleton<IFeatureModule>(module);
        }
        
        return builder;
    }

    public static WebApplication MapFeatureModules(this WebApplication app)
    {
        var modules = app.Services.GetServices<IFeatureModule>();
        foreach (var module in modules)
        {
            module.MapEndpoints(app);
        }
        return app;
    }
}