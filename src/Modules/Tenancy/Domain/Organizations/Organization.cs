using Relay.Modules.Tenancy.App.Organizations.Create;

namespace Relay.Modules.Tenancy.Domain.Organizations;

public sealed class Organization
{
    private Organization() { }
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public OrganizationStatus Status { get; private set; }

    public static Organization Create(string name, string slug)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new OrganizationNotCreatedException("Organization name is required.");

        if (string.IsNullOrWhiteSpace(slug))
            throw new OrganizationNotCreatedException("Organization slug is required.");

        return new Organization
        {
            Name = name.Trim(),
            Slug = slug.Trim().ToLowerInvariant(),
            Status = OrganizationStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
