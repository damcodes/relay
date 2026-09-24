using Microsoft.Extensions.DependencyInjection;

namespace Relay.Common.App;

public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetService<IQueryHandler<TQuery, TResult>>()
            ?? throw new InvalidOperationException($"No handler registered for query type {typeof(TQuery).Name}");
        
        return await handler.HandleAsync(query, cancellationToken);
    }
}