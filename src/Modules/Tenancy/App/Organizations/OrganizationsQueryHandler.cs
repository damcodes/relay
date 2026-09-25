using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Get.Queries;
using Relay.Modules.Tenancy.App.Organizations.Get.Exceptions;
using Relay.Modules.Tenancy.Infrastructure;
using Relay.Modules.Tenancy.App.Organizations.Get.Results;

namespace Relay.Modules.Tenancy.App.Organizations;

public class OrganizationsQueryHandler(TenancyDbContext tenancyContext) 
    : IQueryHandler<GetByIdQuery, OrganizationDetails>
{
    private readonly TenancyDbContext _tenancyContext = tenancyContext;

    public async Task<OrganizationDetails> HandleAsync(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var org = await _tenancyContext.Organizations
            .FindAsync([query.Id], cancellationToken: cancellationToken)
            .ConfigureAwait(false)
                ?? throw new OrganizationNotFoundException();
        return new OrganizationDetails(org.Id, org.Name, org.Slug, org.Status.ToString(), org.CreatedAt);
    }
}