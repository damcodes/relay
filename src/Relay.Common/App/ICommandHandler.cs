namespace Relay.Common.App;

public interface ICommandHandler<TCommand, UData>
{
    Task<UData> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand>
{ 
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}
