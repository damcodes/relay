namespace Relay.Modules.Tenancy.Presentation.Organizations.Dtos;

public record OrganizationDetailsResponse(int Id, string Name, string Slug, string Status, DateTime CreatedAt);