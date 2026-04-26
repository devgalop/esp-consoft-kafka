# Integración .NET con Apache Kafka

Este proyecto es una demostración básica de cómo integrar una aplicación .NET con Apache Kafka para enviar y recibir mensajes. Utiliza la biblioteca Confluent.Kafka para facilitar la comunicación con Kafka.

Para este ejercicio, se instalará Apache Kafka en una máquina tipo servidor y se configurará para que la aplicación .NET pueda interactuar con él.

**NOTA**: Este proyecto es con fines educativos y no está destinado para producción. Si bien Kafka puede ser instalado mediante Docker o directamente desde un cluster para facilitar su uso, en este caso se optará por una instalación tradicional en una máquina virtual para una experiencia más cercana a un entorno de producción.

## Prerequisitos

- .NET 10
- Maquina virtual - Ubuntu server 25.10
- Apache Kafka 2.13-4.2.0
- Java 17+

## Arquitectura

Este proyecto se realiza en una arquitectura de monolito, con una arquitectura de capas verticales basadas en features.

```bash
devgalop.lrn.kafka/
├── Features/
│   ├── Producer/
│   │   ├── ProducerEndpoint.cs
│   │   └── ProducerHandler.cs
│   ├── Consumer/
│   │   ├── ConsumerEndpoint.cs
│   │   └── ConsumerHandler.cs
│   ├── Shared/
│   │   ├── IPublisher.cs
│   │   ├── IConsumer.cs
├── Infrastructure/
│   ├── Kafka/
│   │   ├── Publisher/
│   │   │   ├── KafkaPublisher.cs
│   │   ├── Consumer/
│   │   │   ├── KafkaConsumer.cs
├── Program.cs
```

## Configuración de ambiente

Para configurar el ambiente de desarrollo, valide el archivo [SETUP.md](./SETUP.md) para obtener instrucciones detalladas sobre la instalación de Apache Kafka en una máquina virtual con Ubuntu Server.

## Referencias

Si desea profundizar en la integración de .NET con Apache Kafka, se recomienda revisar la documentación oficial de Confluent.Kafka y los recursos disponibles en el sitio web de Apache Kafka. [Confluent.Kafka Documentation](https://developer.confluent.io/get-started/dotnet/) y [Apache Kafka Documentation](https://kafka.apache.org/)

Para la instalación de Apache Kafka en Ubuntu Server, se recomienda seguir la guía oficial [Quick Start](https://kafka.apache.org/quickstart/)
