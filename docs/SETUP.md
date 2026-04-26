# Configuración de entorno de desarrollo

## Variables de entorno

- `KAFKA_SERVER`: Dirección IP o nombre de host del servidor Kafka.
- `KAFKA_PORT`: Puerto en el que Kafka está escuchando (por defecto, suele ser 9092).
- `KAFKA_TOPIC`: Nombre del topic que se utilizará para enviar y recibir mensajes (por ejemplo, `my-topic-events`).

**IMPORTANTE**: en el ambiente de desarrollo puedes configurar estas variables de entorno a través de secretos de usuarios o utilizando el archivo de configuración de tu IDE como `launchSettings.json` o en `appsettings.Development.json` para facilitar su uso durante el desarrollo. Asegúrate de no incluir información sensible en el código fuente o en archivos de configuración que puedan ser compartidos públicamente.

**RECOMENDACIÓN**: Configure en las variables de entorno mediante secretos de usuario de la siguiente manera:

```bash
dotnet user-secrets set "KAFKA_SERVER" "localhost"
dotnet user-secrets set "KAFKA_PORT" "9092"
dotnet user-secrets set "KAFKA_TOPIC" "my-topic-events"
```

## Instalación de Kafka en Ubuntu Server

Para la instalación de Apache Kafka en Ubuntu Server, se recomienda seguir la guía oficial [Quick Start](https://kafka.apache.org/quickstart/)

- Descargar e instalar Java 17+ (requisito para Kafka) [Link de descarga](https://www.oracle.com/java/technologies/downloads/)

- Descargar Kafka

```bash
wget https://www.apache.org/dyn/closer.cgi?path=/kafka/4.2.0/kafka_2.13-4.2.0.tgz
tar -xzf kafka_2.13-4.2.0.tgz
cd kafka_2.13-4.2.0
```

- Modificar configuración de listeners en `config/server.properties` para que Kafka escuche en la dirección IP de la máquina virtual y el puerto configurado en las variables de entorno.

```bash
nano config/server.properties
`
# Modificar la línea de listeners para que Kafka escuche en la dirección IP y puerto configurados
# KAFKA_SERVER debe ser la dirección IP de la máquina virtual
# KAFKA_PORT debe ser el puerto configurado (por defecto, 9092)
advertised.listeners=PLAINTEXT://KAFKA_SERVER:KAFKA_PORT
```

- Iniciar servidor Kafka

```bash
KAFKA_CLUSTER_ID="$(bin/kafka-storage.sh random-uuid)"
bin/kafka-storage.sh format --standalone -t $KAFKA_CLUSTER_ID -c config/server.properties
bin/kafka-server-start.sh config/server.properties
```

- Crear topic para enviar y recibir mensajes

```bash
# Modificar la línea de listeners para que Kafka escuche en la dirección IP y puerto configurados
# KAFKA_SERVER debe ser la dirección IP de la máquina virtual
# KAFKA_PORT debe ser el puerto configurado (por defecto, 9092)
bin/kafka-topics.sh --create --topic my-topic-events --bootstrap-server KAFKA_SERVER:KAFKA_PORT
```
