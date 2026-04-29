namespace devgalop.lrn.kafka.Features.Notifications.Contracts;

public interface IMessage
{
    string Serialize();
    string GetIdentifier();
}