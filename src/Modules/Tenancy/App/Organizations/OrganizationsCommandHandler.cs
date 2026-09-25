using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Create.Commands;
using Relay.Modules.Tenancy.App.Organizations.Create.Results;
using Relay.Modules.Tenancy.Domain.Organizations;
using Relay.Modules.Tenancy.Infrastructure;

namespace Relay.Modules.Tenancy.App.Organizations;

public class OrganizationsCommandHandler(TenancyDbContext context) 
    : ICommandHandler<CreateOrganizationCommand, CreateOrganizationResult>
{
    private readonly TenancyDbContext _context = context;

    public async Task<CreateOrganizationResult> HandleAsync(CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var org = Organization.Create(command.Name);
        _context.Organizations.Add(org);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateOrganizationResult(org.Id, org.Name, org.Slug, org.Status.ToString(), org.CreatedAt);;
    }
}