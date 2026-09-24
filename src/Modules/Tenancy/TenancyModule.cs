namespace Relay.Modules.Tenancy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relay.Common.App;
using Relay.Modules.Tenancy.Infrastructure;

public static class TenancyModule
{
    public static IServiceCollection AddTenancyModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string connectionString = configuration.GetSection(TenancyOptions.SectionName).GetValue<string>("ConnectionStrings:RelayAppDb")
            ?? throw new InvalidOperationException("RelayAppDb connection string not found in IConfiguration.");

        return services
            .Configure<TenancyOptions>(configuration.GetSection(TenancyOptions.SectionName))
            .AddDbContext<TenancyDbContext>(options =>
                options.UseNpgsql(connectionString, options => options.MigrationsAssembly(typeof(TenancyDbContext).Assembly)))
            .RegisterHandlers();
    }

    private static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        var handlerTypes = typeof(TenancyModule).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Concrete = t, Interface = i })
            .Where(x =>
            {
                if (!x.Interface.IsGenericType)
                    return false;
                Type interfaceType = x.Interface.GetGenericTypeDefinition();
                return interfaceType == typeof(ICommandHandler<,>) 
                    || interfaceType == typeof(IQueryHandler<,>);
            });

        foreach (var handler in handlerTypes)
            services.AddTransient(handler.Interface, handler.Concrete);

        return services;
    }
}