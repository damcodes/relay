namespace Relay.Modules.Tenancy.Domain;

public class Organization
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Status { get; set; }
}
