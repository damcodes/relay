using Microsoft.Extensions.DependencyInjection;

namespace Relay.Common.App;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetService<ICommandHandler<TCommand>>() 
            ?? throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");

        await handler.HandleAsync(command, cancellationToken);
    }
}