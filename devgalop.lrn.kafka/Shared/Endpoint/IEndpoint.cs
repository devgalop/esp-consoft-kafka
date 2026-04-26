using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace devgalop.lrn.kafka.Shared.Endpoint;

/// <summary>
/// Interfaz que define el contrato para los endpoints en la aplicación web. Cada endpoint debe implementar esta interfaz para ser registrado y mapeado correctamente en la aplicación. Proporciona un método para definir el mapeo de rutas y acciones asociadas a cada endpoint.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Define el mapeo de endpoints en la aplicación web
    /// </summary>
    /// <param name="app">builder de aplicación</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}

public static class EndpointExtensions
{
    /// <summary>
    /// Registra las dependencias de todos los endpoints en el contenedor de servicios
    /// </summary>
    /// <param name="builder">builder de aplicación</param>
    /// <returns>Builder de aplicación con los endpoints registrados</returns>
    public static WebApplicationBuilder AddEndpoints(this WebApplicationBuilder builder)
    {
        // Registrar todos los tipos que implementen IEndpoint en el contenedor de servicios
        var serviceDescriptors = Assembly.GetExecutingAssembly()
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                            typeof(IEndpoint).IsAssignableFrom(type))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        builder.Services.TryAddEnumerable(serviceDescriptors);
        return builder;
    }

    /// <summary>
    /// Mapea todos los endpoints registrados en la aplicación web
    /// </summary>
    /// <param name="app">aplicaión web</param>
    /// <returns>Aplicación web con los endpoints mapeados</returns>
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // Obtener todos los servicios registrados que implementen IEndpoint
        var endpoints = app.Services.GetServices<IEndpoint>();

        // Mapear cada endpoint en la aplicación web
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
