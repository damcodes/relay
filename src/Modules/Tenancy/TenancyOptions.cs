namespace Relay.Modules.Tenancy;

public sealed class TenancyOptions
{
    public const string SectionName = "Tenancy";
    public required string RelayAppDb { get; init; }
}