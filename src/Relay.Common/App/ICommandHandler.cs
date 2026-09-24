namespace Relay.Common.App;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand>
{ 
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}
