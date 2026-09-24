using Microsoft.EntityFrameworkCore;
using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Get;
using Relay.Modules.Tenancy.Domain.Organizations;
using Relay.Modules.Tenancy.Infrastructure;

namespace Relay.Modules.Tenancy.App.Organizations;

public class OrganizationsQueryHandler(TenancyDbContext tenancyContext) 
    : IQueryHandler<GetByIdQuery, Organization>
{
    private readonly TenancyDbContext _tenancyContext = tenancyContext;

    public async Task<Organization> HandleAsync(GetByIdQuery query, CancellationToken cancellationToken)
    {
        return await _tenancyContext.Organizations.FindAsync([query.Id], cancellationToken: cancellationToken).ConfigureAwait(false) 
            ?? throw new OrganizationNotFoundException();
    }
}