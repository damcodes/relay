using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Relay.Modules.Tenancy.Data;

namespace Relay.Modules.Tenancy;

public static class TenancyModule
{
    public static IServiceCollection AddTenancyModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<TenancyOptions>(configuration.GetSection(TenancyOptions.SectionName));
        string connectionString = configuration.GetSection(TenancyOptions.SectionName).GetValue<string>("RelayAppDb")
            ?? throw new InvalidOperationException("RelayAppDb connection string not found in IConfiguration.");
        services.AddDbContext<TenancyDbContext>(options => 
            options.UseNpgsql(connectionString));

        return services;
    }
}