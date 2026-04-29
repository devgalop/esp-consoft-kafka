using devgalop.lrn.kafka.Features;
using devgalop.lrn.kafka.Infrastructure.Kafka.Consumer;
using devgalop.lrn.kafka.Infrastructure.Kafka.Publisher;
using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>();

builder.AddMediator()
       .AddFeatureModules()
       .AddKafkaPublisher()
       .AddKafkaConsumer();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapFeatureModules();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
await app.RunAsync();