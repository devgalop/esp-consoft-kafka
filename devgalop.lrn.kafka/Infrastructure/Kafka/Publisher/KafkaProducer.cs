using Confluent.Kafka;
using devgalop.lrn.kafka.Features.Shared;
using devgalop.lrn.kafka.Infrastructure.Kafka.Exceptions;
using devgalop.lrn.kafka.Shared.Messages;
using devgalop.lrn.kafka.Shared.Options;

namespace devgalop.lrn.kafka.Infrastructure.Kafka.Publisher;

public class KafkaProducer(
    KafkaOptions options
) : IPublisher
{
    public async Task PublishAsync(IMessage message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = $"{options.Host}:{options.Port}",
            Acks = Acks.All
        };        
        using var producer = new ProducerBuilder<string, string>(config).Build();
        var deliveryResult = await producer.ProduceAsync(
            options.Topic,
            new Message<string, string> { Key = message.GetIdentifier(), Value = message.Serialize() });

        if (deliveryResult.Status != PersistenceStatus.Persisted)
        {
            throw new MessageDeliveryException(deliveryResult.Status.ToString(), deliveryResult.Topic, deliveryResult.Partition.Value, deliveryResult.Offset.Value);
        }

        Console.WriteLine($"Evento publicado en topic= {deliveryResult.Topic}: key = {message.GetIdentifier()} partition = {deliveryResult.Partition} offset = {deliveryResult.Offset}");
    }
}


public static class KafkaProducerExtensions
{
    /// <summary>
    /// Agrega el servicio de KafkaProducer al contenedor de dependencias.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web con el servicio de KafkaProducer agregado.</returns>
    public static WebApplicationBuilder AddKafkaProducer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPublisher, KafkaProducer>();
        return builder;
    }
}
