# Architecture

## Overview

This project is a .NET 10 monolithic application that demonstrates integration with Apache Kafka for message Publishing and Consumption. It uses a **Vertical Slice Architecture** with feature-based modules.

## Technology Stack

- **.NET 10** - Runtime
- **Confluent.Kafka 2.14.0** - Kafka client library
- **Entity Framework Core 10.0.7** - ORM
- **SQLite** - Persistence
- **Microsoft.AspNetCore.OpenApi** - OpenAPI/Swagger

## Folder Structure

```bash
devgalop.lrn.kafka/
├── Features/                    # Vertical slices (feature modules)
│   ├── Notifications/         # Publish messages feature
│   │   ├── Contracts/          # Abstractions (IPublisher, IMessage)
│   │   ├── Handlers/          # Command handlers
│   │   ├── Endpoints/         # HTTP endpoints
│   │   ├── Models/            # Domain models
│   │   ├── Exceptions/        # Feature-specific exceptions
│   │   └── NotificationsFeature.cs
│   ├── Consumer/              # Consume messages feature
│   │   ├── Contracts/
│   │   ├── Handlers/
│   │   ├── Endpoints/
│   │   └── ConsumerFeature.cs
│   ├── Logging/               # Logging feature (persistence)
│   ├── IFeatureModule.cs      # Feature module interface
│   └── FeatureModuleExtensions.cs
├── Infrastructure/            # External integrations
│   ├── Kafka/
│   │   ├── Publisher/        # Kafka producer implementation
│   │   └── Consumer/         # Kafka consumer implementation
│   └── Persistence/           # Database context & repositories
├── Shared/                    # Cross-cutting concerns
│   ├── Base/                  # Base classes (RequestDto)
│   ├── Endpoint/              # HTTP endpoint abstraction
│   ├── Exceptions/            # Shared exceptions
│   ├── Mediator/              # Mediator pattern implementation
│   └── Options/               # Configuration options (KafkaOptions)
├── Program.cs                 # Application entry point
└── devgalop.lrn.kafka.csproj
```

## Architectural Patterns

### Vertical Slice Architecture

Each feature is a self-contained module implementing `IFeatureModule`:

```csharp
public interface IFeatureModule
{
    void RegisterDependencies(IServiceCollection services);
    void MapEndpoints(IEndpointRouteBuilder app);
}
```

Features are registered in `FeatureModuleExtensions` and auto-discovered at startup.

### Mediator Pattern

Commands are dispatched via `IMediator`:

```csharp
public interface IMediator
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
}
```

### Dependency Injection

All dependencies are registered via extension methods:

- `AddKafkaPublisher()` - Producer and configuration
- `AddKafkaConsumer()` - Consumer and configuration
- `AddMediator()` - Command mediator
- `AddLoggingPersistence()` - Database context

## Key Design Principles

- **SOLID** - Single responsibility, open/closed, Liskov substitution, interface segregation, dependency inversion
- **DRY** - Avoid duplication through shared contracts and extensions
- **KISS** - Simple, readable implementations

## Configuration

Environment variables required:

- `KAFKA_SERVER` - Kafka hostname
- `KAFKA_PORT` - Kafka port
- `KAFKA_TOPIC` - Topic name

Or via `appsettings.json` / user secrets.

## Running the Project

```bash
dotnet run
```

In development, OpenAPI docs available at `/openapi/v1.json`.
