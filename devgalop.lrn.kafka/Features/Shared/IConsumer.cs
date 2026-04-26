namespace devgalop.lrn.kafka.Features.Shared;

public interface IConsumer
{
    Task ConsumeAsync(int numberOfMessages);
}
