using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Users.UpdateUserRoles;

public sealed class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<UpdateUserRolesCommandHandler> _logger;

    public UpdateUserRolesCommandHandler(IUnitOfWork uow, ILogger<UpdateUserRolesCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        User user = (await _uow.Users.FindUserWithRolesAsync(request.Id,
                        cancellationToken)) ??
                    throw new KsNotFoundException(request.Id.ToString());
        
        List<Role> roles = new();

        foreach (string roleName in request.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName, cancellationToken);
            
            if (role == null)
                throw new KsNotFoundException("Role not found");
            
            roles.Add(role);
        }
        
        try
        {
            user.ClearRoles();
            
            user.AssignRoles(roles);
            
            await _uow.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on updating user roles: {ErrorMessage}", ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
            throw;
        }
    }
}