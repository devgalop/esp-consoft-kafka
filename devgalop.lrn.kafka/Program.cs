using devgalop.lrn.kafka.Features.Shared;
using devgalop.lrn.kafka.Infrastructure.Kafka.Consumer;
using devgalop.lrn.kafka.Infrastructure.Kafka.Publisher;
using devgalop.lrn.kafka.Shared.Endpoint;
using devgalop.lrn.kafka.Shared.Mediator;
using devgalop.lrn.kafka.Shared.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>();

builder.AddEndpoints()
       .AddMediator()
       .AddKafkaOptions()
       .AddCommonDependencies()
       .AddKafkaProducer()
       .AddKafkaConsumer();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
await app.RunAsync();
