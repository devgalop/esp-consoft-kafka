using Confluent.Kafka;
using devgalop.lrn.kafka.Features.Shared;
using devgalop.lrn.kafka.Shared.Options;

namespace devgalop.lrn.kafka.Infrastructure.Kafka.Consumer;

public class KafkaConsumer(
    KafkaOptions options
) : IConsumer
{
    public async Task ConsumeAsync(int numberOfMessages)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = $"{options.Host}:{options.Port}",
            GroupId = "devgalop-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false 
        };
        int messageProcessed = 0;
        bool noMessagesReceived = false;
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(options.Topic);
        try
        {
            while (messageProcessed < numberOfMessages)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(5));

                if (result == null) 
                {
                    Console.WriteLine("No se recibieron mensajes en el intervalo de tiempo especificado.");
                    messageProcessed = numberOfMessages;
                    Console.WriteLine("Finaliza procesamiento de los mensajes solicitados por falta de mensajes en el topic.");
                    noMessagesReceived = true;
                    continue;
                }

                Console.WriteLine($"Mensaje: {result.Message.Value}");
                messageProcessed++;
            }
            if (!noMessagesReceived)
            {
                consumer.Commit();
                Console.WriteLine("Mensajes procesados y offsets confirmados.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            consumer.Close();
        }
    }
}

public static class KafkaConsumerExtensions
{
    /// <summary>
    /// Agrega el servicio de KafkaConsumer al contenedor de dependencias.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con el servicio de KafkaConsumer agregado.</returns>
    public static WebApplicationBuilder AddKafkaConsumer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConsumer, KafkaConsumer>();
        return builder;
    }
}

