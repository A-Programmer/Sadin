using Microsoft.AspNetCore.Authorization;
using Sadin.Application.Roles.CreateRole;
using Sadin.Application.Roles.DeleteRole;
using Sadin.Application.Roles.GetPaginatedRoles;
using Sadin.Application.Roles.GetRoleById;
using Sadin.Application.Roles.UpdateRole;
using Sadin.Presentation.Controllers.BaseControllers;

namespace Sadin.Presentation.Controllers.AdminControllers;

[Authorize(Roles = "admin")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = SwaggerGroupLabels.Admin)]
public sealed class RolesController(ISender sender)  : SecureBaseController(sender)
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedList<RolesListItemResponse>>> GetAsync(
        [FromQuery] SearchRequestOptions options,
        CancellationToken cancellationToken = default)
    {
        GetPaginatedRolesQuery query = new(options);
        
        PaginatedList<RolesListItemResponse> result = await Sender.Send(query,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetByIdAsync")]
    [Produces(typeof(RoleItemResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleItemResponse>> GetAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        GetRoleByIdQuery query = new(id);

        RoleItemResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Produces(typeof(RoleUpdateResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleUpdateResponse>> PutAsync(Guid id,
        RoleUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        RoleUpdateCommand command = new(id, request.Name, request.Description);

        RoleUpdateResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleUpdateResponse>> PostAsync(
        CreateRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        CreateRoleCommand command = new(request.Name, request.Description);

        CreateRoleResponse result = await Sender.Send(command, cancellationToken);

        return CreatedAtRoute(routeName: "GetByIdAsync" ,result);
    }

    [HttpDelete("{id}")]
    [Produces(typeof(DeleteRoleResponse))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid id,
        CancellationToken cancellationToken)
    {
        DeleteRoleCommand command = new(id);

        return Ok(await Sender.Send(command, cancellationToken));
    }
}