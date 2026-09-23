using Microsoft.EntityFrameworkCore;
using Relay.Modules.Tenancy.Domain.Organizations;

namespace Relay.Modules.Tenancy.Infrastructure.Configurations;

public static class OrganizationConfiguration
{
    public static void ConfigureOrganization(this ModelBuilder builder)
    {
        builder.Entity<Organization>()
            .Property(org => org.Status)
            .HasConversion<string>();

        builder.Entity<Organization>()
            .HasIndex(org => org.Slug)
            .IsUnique();
    }
}