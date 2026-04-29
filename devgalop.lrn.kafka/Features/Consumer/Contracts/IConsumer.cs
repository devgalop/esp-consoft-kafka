namespace devgalop.lrn.kafka.Features.Consumer.Contracts;

public interface IConsumer
{
    Task ConsumeAsync(int numberOfMessages);
}