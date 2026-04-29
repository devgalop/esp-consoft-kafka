using Confluent.Kafka;
using devgalop.lrn.kafka.Features.Notifications.Contracts;
using devgalop.lrn.kafka.Features.Notifications.Exceptions;
using devgalop.lrn.kafka.Shared.Options;
using Microsoft.Extensions.Logging;

namespace devgalop.lrn.kafka.Infrastructure.Kafka.Publisher;

public class KafkaPublisher(
    IProducer<string, string> producer,
    KafkaOptions options,
    ILogger<KafkaPublisher> logger
) : IPublisher
{
    public async Task PublishAsync(IMessage message)
    {
        logger.LogInformation("Publishing message to topic {Topic}", options.Topic);
        
        var deliveryResult = await producer.ProduceAsync(
            options.Topic,
            new Message<string, string> { Key = message.GetIdentifier(), Value = message.Serialize() });

        if (deliveryResult.Status != PersistenceStatus.Persisted)
        {
            throw new MessageDeliveryException(
                deliveryResult.Status.ToString(), 
                deliveryResult.Topic, 
                deliveryResult.Partition.Value, 
                deliveryResult.Offset.Value);
        }

        logger.LogInformation(
            "Event published to topic={Topic}: key={Key} partition={Partition} offset={Offset}",
            deliveryResult.Topic, 
            message.GetIdentifier(), 
            deliveryResult.Partition, 
            deliveryResult.Offset);
    }
}

public static class KafkaPublisherExtensions
{
    public static WebApplicationBuilder AddKafkaPublisher(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<KafkaOptions>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            string host = config["KAFKA_SERVER"] ?? throw new Shared.Exceptions.MissingConfigurationException("KAFKA_SERVER");
            int port = int.TryParse(config["KAFKA_PORT"], out int p) ? p : throw new Shared.Exceptions.MissingConfigurationException("KAFKA_PORT");
            string topic = config["KAFKA_TOPIC"] ?? throw new Shared.Exceptions.MissingConfigurationException("KAFKA_TOPIC");
            return new KafkaOptions(host, port, topic);
        });

        builder.Services.AddSingleton<IProducer<string, string>>(sp =>
        {
            var options = sp.GetRequiredService<KafkaOptions>();
            var config = new ProducerConfig
            {
                BootstrapServers = $"{options.Host}:{options.Port}",
                Acks = Acks.All
            };
            return new ProducerBuilder<string, string>(config).Build();
        });

        builder.Services.AddSingleton<IPublisher, KafkaPublisher>();
        return builder;
    }
}