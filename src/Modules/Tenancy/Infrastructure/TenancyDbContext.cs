namespace Relay.Modules.Tenancy.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Relay.Modules.Tenancy.Domain;
using Relay.Modules.Tenancy.Domain.Organizations;
using Relay.Modules.Tenancy.Infrastructure.Configurations;

public class TenancyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Membership> Memberships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureOrganization();
    }
}
