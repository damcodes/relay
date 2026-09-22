using Microsoft.EntityFrameworkCore;

namespace Relay.Modules.Tenancy.Domain;

[Index(nameof(OrganizationId), nameof(UserId), IsUnique = true)]
public class Membership
{
    public int OrganizationId { get; set; }
    public int UserId { get; set; }
    public required string Role { get; set; }
    public required string Status { get; set; }
    public DateTime JoinedAt { get; set; }
}