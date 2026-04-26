namespace devgalop.lrn.kafka.Shared.Messages;

public interface IMessage
{
    /// <summary>
    /// Serializa el mensaje a una cadena de texto que pueda ser enviada a Kafka.
    /// </summary>
    /// <returns>Una representación en cadena del mensaje.</returns>
    string Serialize();

    /// <summary>
    /// Obtiene un identificador único para el mensaje, que puede ser utilizado como clave en Kafka.
    /// </summary>
    /// <returns>Una cadena que representa el identificador único del mensaje.</returns>
    string GetIdentifier();
}
