namespace devgalop.lrn.kafka.Shared.Mediator;

public interface ICommand
{
    string Serialize();
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

public interface IMediator
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
}

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }
}

public static class MediatorExtensions
{
    public static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMediator, Mediator>();
        return builder;
    }
}