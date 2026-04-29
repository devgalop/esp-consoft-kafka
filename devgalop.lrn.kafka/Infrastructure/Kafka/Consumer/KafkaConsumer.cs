using Confluent.Kafka;
using devgalop.lrn.kafka.Features.Consumer.Contracts;
using devgalop.lrn.kafka.Shared.Options;
using Microsoft.Extensions.Logging;

namespace devgalop.lrn.kafka.Infrastructure.Kafka.Consumer;

public sealed class KafkaConsumer(
    IConsumer<Ignore, string> consumer,
    KafkaOptions options,
    ILogger<KafkaConsumer> logger
) : IConsumer, IDisposable
{
    private bool _disposed = false;

    public async Task ConsumeAsync(int numberOfMessages)
    {
        logger.LogInformation("Starting to consume {Count} messages from topic {Topic}", numberOfMessages, options.Topic);
        
        consumer.Subscribe(options.Topic);
        int messageProcessed = 0;
        bool noMessagesReceived = false;

        try
        {
            while (messageProcessed < numberOfMessages)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(5));

                if (result == null) 
                {
                    logger.LogWarning("No messages received within timeout period");
                    messageProcessed = numberOfMessages;
                    logger.LogInformation("Finished processing - no more messages in topic");
                    noMessagesReceived = true;
                    continue;
                }

                logger.LogInformation("Message received: {Message}", result.Message.Value);
                messageProcessed++;
            }
            
            if (!noMessagesReceived)
            {
                consumer.Commit();
                logger.LogInformation("Messages processed and offsets committed");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during message consumption");
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            consumer.Close();
            consumer.Dispose();
            _disposed = true;
        }
    }
}

public static class KafkaConsumerExtensions
{
    public static WebApplicationBuilder AddKafkaConsumer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConsumer<Ignore, string>>(sp =>
        {
            var options = sp.GetRequiredService<KafkaOptions>();
            var config = new ConsumerConfig
            {
                BootstrapServers = $"{options.Host}:{options.Port}",
                GroupId = "devgalop-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false 
            };
            return new ConsumerBuilder<Ignore, string>(config).Build();
        });

        builder.Services.AddSingleton<IConsumer, KafkaConsumer>();
        return builder;
    }
}