using Microsoft.AspNetCore.Mvc;
using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Create;
using Relay.Modules.Tenancy.App.Organizations.Get;
using Relay.Modules.Tenancy.Domain.Organizations;

namespace Relay.Modules.Tenancy.Presentation.Organizations;

[ApiController]
[Route("orgs")]
public class OrganizationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher = commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher = queryDispatcher;

    [HttpPost]
    public async Task<IActionResult> CreateOrganization(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateOrganizationCommand(request.Name);
            int newOrgId = await _commandDispatcher.DispatchAsync<CreateOrganizationCommand, int>(command, cancellationToken);

            var query = new GetByIdQuery(newOrgId);
            var newOrg = await _queryDispatcher.DispatchAsync<GetByIdQuery, Organization>(query, cancellationToken);
            var newOrgResponse = new CreateOrganizationResponse(newOrg.Id, newOrg.Name, newOrg.Slug, newOrg.CreatedAt, newOrg.Status.ToString());
            return StatusCode(201, newOrgResponse);
        }
        catch (OrganizationNotCreatedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OrganizationNotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"An unexpected server error occurred. {ex.Message}"});
        }
    }
}