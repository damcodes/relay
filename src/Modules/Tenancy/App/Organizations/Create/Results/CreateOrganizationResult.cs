namespace Relay.Modules.Tenancy.App.Organizations.Create.Results;

public record CreateOrganizationResult(int Id, string Name, string Slug, string Status, DateTime CreatedAt);