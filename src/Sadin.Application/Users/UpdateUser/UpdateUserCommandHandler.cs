using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler
    : ICommandHandler<UpdateUserCommand,
    UpdateUserResponse>
{
    private readonly IUnitOfWork _uow;

    public UpdateUserCommandHandler(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User user = (await _uow.Users.FindUserWithRolesAsync(request.Id,
            cancellationToken)) ??
                     throw new KsNotFoundException(request.UserName);

        if (await AreInformationInUse(request, cancellationToken))
            throw new KsDuplicatedUserException("A user with the same information");

        user.Update(request.UserName,
            request.Email,
            request.PhoneNumber);

        List<Role> roles = new();
        
        foreach (string roleName in request.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName,
                cancellationToken) ?? throw new KsNotFoundException(roleName);
            
            roles.Add(role);
        }
        
        user.ClearRoles();
        user.AssignRoles(roles);

        await _uow.CommitAsync(cancellationToken);

        return new UpdateUserResponse(request.Id);
    }

    private async Task<bool> AreInformationInUse(UpdateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        return await _uow
            .Users
            .IsEmailInUseAsync(request.Id,
                request.Email,
                cancellationToken) ||
               
        await _uow
                .Users.IsPhoneNumberInUseAsync(request.Id,
                    request.PhoneNumber,
                    cancellationToken) ||

            await _uow
                .Users.IsUserNameInUseAsync(request.Id,
                    request.UserName,
                    cancellationToken);
    }
}