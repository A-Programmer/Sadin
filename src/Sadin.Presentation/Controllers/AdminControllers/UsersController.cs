using Microsoft.AspNetCore.Authorization;
using Sadin.Application.Users.CreateUser;
using Sadin.Application.Users.DeleteUser;
using Sadin.Application.Users.GetPaginatedUsers;
using Sadin.Application.Users.GetUserById;
using Sadin.Application.Users.ResetPassword;
using Sadin.Application.Users.UpdateUser;
using Sadin.Application.Users.UpdateUserRoles;
using Sadin.Presentation.Controllers.BaseControllers;

namespace Sadin.Presentation.Controllers.AdminControllers;

[Authorize(Roles = "admin")]
[ApiExplorerSettings(GroupName = SwaggerGroupLabels.Admin)]
public sealed class UsersController(ISender sender) : SecureBaseController(sender)
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<UsersListItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<UsersListItemResponse>>> GetAllAsync(
        [FromQuery] SearchRequestOptions options,
        CancellationToken cancellationToken = default)
    {
        GetPaginatedUsersQuery query = new(options);

        PaginatedList<UsersListItemResponse> result =
            await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> GetAsync(Guid id,
        CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(id);

        UserResponse result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateUserResponse>> PostAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        CreateUserCommand command = new(request.UserName,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.ConfirmPassword,
            request.Roles);

        CreateUserResponse result = await Sender.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateUserResponse>> PutAsync(Guid id,
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        UpdateUserCommand command = new(id, request.UserName, request.Email, request.PhoneNumber, request.Roles);

        UpdateUserResponse result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }
    
    
    [HttpPut("{id}/password/reset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateUserResponse>> PutAsync(Guid id,
        ResetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        ResetPasswordCommand command = new(id, request.NewPassword);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    
    [HttpPut("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateUserResponse>> PutAsync(Guid id,
        UpdateUserRolesRequest request,
        CancellationToken cancellationToken)
    {
        UpdateUserRolesCommand command = new(id, request.Roles);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(Guid id,
        CancellationToken cancellationToken)
    {
        DeleteUserCommand command = new(id);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
}