namespace Relay.Modules.Tenancy.Domain;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(OrganizationId), nameof(UserId))]
public class Membership
{
    public int OrganizationId { get; set; }
    public int UserId { get; set; }
    public required string Role { get; set; }
    public required string Status { get; set; }
    public DateTime JoinedAt { get; set; }
}