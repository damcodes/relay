namespace Relay.Modules.Tenancy.Domain.Organizations;

public sealed class OrganizationNotCreatedException(string message) : Exception(message);