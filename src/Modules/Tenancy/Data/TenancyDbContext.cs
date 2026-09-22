namespace Relay.Modules.Tenancy.Data;

using Microsoft.EntityFrameworkCore;
using Relay.Modules.Tenancy.Domain;

public class TenancyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Membership> Memberships { get; set; }
}
