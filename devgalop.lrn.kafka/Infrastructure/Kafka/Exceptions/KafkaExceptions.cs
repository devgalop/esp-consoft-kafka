namespace devgalop.lrn.kafka.Infrastructure.Kafka.Exceptions;

public class MessageDeliveryException(string status, string topic, int partition, long offset) 
: Exception($"La entrega del mensaje falló. Estado: {status}, Topic: {topic}, Partición: {partition}, Offset: {offset}")
{
}
