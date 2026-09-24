namespace Relay.Modules.Tenancy.App.Organizations.Create;

public sealed class OrganizationNotCreatedException(string message) : Exception(message);