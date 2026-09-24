namespace Relay.Common.App;

public interface ICommandDispatcher
{
    Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken);
    Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken);
}