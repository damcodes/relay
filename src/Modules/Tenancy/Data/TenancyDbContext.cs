namespace Relay.Modules.Tenancy.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Relay.Modules.Tenancy.Domain;

public class TenancyDbContext(IOptions<TenancyOptions> configuration) : DbContext
{
    private readonly TenancyOptions _config = configuration.Value;
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Membership> Memberships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_config.RelayAppDb);
    }
}
