namespace Relay.Modules.Tenancy;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relay.Modules.Tenancy.Infrastructure;

public static class TenancyModule
{
    public static IServiceCollection AddTenancyModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<TenancyOptions>(configuration.GetSection(TenancyOptions.SectionName));
        string connectionString = configuration.GetSection(TenancyOptions.SectionName).GetValue<string>("ConnectionStrings:RelayAppDb")
            ?? throw new InvalidOperationException("RelayAppDb connection string not found in IConfiguration.");
        services.AddDbContext<TenancyDbContext>(options => 
            options.UseNpgsql(connectionString, options => options.MigrationsAssembly(typeof(TenancyDbContext).Assembly)));

        return services;
    }
}