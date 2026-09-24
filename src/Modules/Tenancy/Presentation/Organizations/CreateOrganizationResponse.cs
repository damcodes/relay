namespace Relay.Modules.Tenancy.Presentation.Organizations;

public record CreateOrganizationResponse(int Id, string Name, string Slug, DateTime CreatedAt, string Status);