namespace Relay.Modules.Tenancy.App.Organizations.Get.Results;

public record OrganizationDetails(int Id, string Name, string Slug, string Status, DateTime CreatedAt);