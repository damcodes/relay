using Microsoft.AspNetCore.Mvc;
using Relay.Common.App;
using Relay.Modules.Tenancy.App.Organizations.Create.Commands;
using Relay.Modules.Tenancy.App.Organizations.Create.Results;
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
            var newOrg = await _commandDispatcher.DispatchAsync<CreateOrganizationCommand, CreateOrganizationResult>(command, cancellationToken);
            var newOrgResult = new CreateOrganizationResponse(newOrg.Id, newOrg.Name, newOrg.Slug, newOrg.CreatedAt, newOrg.Status);
            return CreatedAtAction(
                actionName: nameof(GetOrganization),
                routeValues: new { id = newOrg.Id },
                value: newOrgResult
            );
        }
        catch (OrganizationNotCreatedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"An unexpected server error occurred. {ex.Message}"});
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganization(int id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetByIdQuery(id);
            var org = await _queryDispatcher.DispatchAsync<GetByIdQuery, Organization>(query, cancellationToken);
            return Ok(org);
        }
        catch (OrganizationNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"An unexpected server error occurred. {ex.Message}"});
        }
    }
}