using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Create;
using Relay.Modules.Tenancy.Domain.Organizations;
using Relay.Modules.Tenancy.Infrastructure;

namespace Relay.Modules.Tenancy.App.Organizations;

public class OrganizationsCommandHandler(TenancyDbContext context) 
    : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly TenancyDbContext _context = context;

    public async Task<int> HandleAsync(CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var slug = CreateSlugFromName(command.Name);
        var org = Organization.Create(command.Name, slug);
        _context.Organizations.Add(org);
        await _context.SaveChangesAsync(cancellationToken);
        return org.Id;
    }

    private static string CreateSlugFromName(string name)
    {
        return string.Join('-', name.Split(' ').Select(word => word.ToLower()));
    }
}