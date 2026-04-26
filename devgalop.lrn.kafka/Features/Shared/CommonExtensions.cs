using devgalop.lrn.kafka.Features.Consumer;
using devgalop.lrn.kafka.Features.Producer;

namespace devgalop.lrn.kafka.Features.Shared;

public static class CommonExtensions
{
    /// <summary>
    /// Agrega las dependencias comunes a la aplicación, como los handlers de productores.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con las dependencias comunes agregadas.</returns>
    public static WebApplicationBuilder AddCommonDependencies(this WebApplicationBuilder builder)
    {
        builder.AddProducerHandler()
               .AddConsumerHandler();
        return builder;
    }

}
