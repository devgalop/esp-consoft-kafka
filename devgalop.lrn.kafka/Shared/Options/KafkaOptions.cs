using devgalop.lrn.kafka.Shared.Exceptions;

namespace devgalop.lrn.kafka.Shared.Options;

public record KafkaOptions(
    string Host,
    int Port,
    string Topic
);

public static class KafkaOptionsExtensions
{
    /// <summary>
    /// Agrega las configuraciones de Kafka al contenedor de las dependencias. Requiere que las siguientes variables de entorno estén definidas:
    /// - KAFKA_SERVER: Ip del servidor de Kafka.
    /// - KAFKA_PORT: Puerto del servidor de Kafka.
    /// - KAFKA_TOPIC: Nombre del topic de Kafka a utilizar.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con las opciones de Kafka agregadas.</returns>
    /// <exception cref="MissingConfigurationException">Se lanza cuando alguna de las variables de entorno necesarias no está definida.</exception>
    public static WebApplicationBuilder AddKafkaOptions(this WebApplicationBuilder builder)
    {
        string host = builder.Configuration["KAFKA_SERVER"] ?? throw new MissingConfigurationException("KAFKA_SERVER");
        int port = int.TryParse(builder.Configuration["KAFKA_PORT"], out int p) ? p : throw new MissingConfigurationException("KAFKA_PORT");
        string topic = builder.Configuration["KAFKA_TOPIC"] ?? throw new MissingConfigurationException("KAFKA_TOPIC");
        KafkaOptions opt = new KafkaOptions(
            host,
            port,
            topic
        );
        builder.Services.AddSingleton(opt);
        return builder;
    }
}
