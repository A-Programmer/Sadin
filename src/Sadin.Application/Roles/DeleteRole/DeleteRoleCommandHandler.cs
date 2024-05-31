using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.DeleteRole;

public sealed class DeleteRoleCommandHandler : CqrsBase, ICommandHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
    
    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await UnitOfWork
            .Roles
            .GetRoleByIdIncludingUsersAsync(request.Id,
                cancellationToken);

        if (role is null)
            throw new KsNotFoundException(request.Id.ToString());

        if (role.Users.Count > 0)
            throw new KsParentHasChildrenException("This role is assigned to some users and you can not delete it, please remove users and then try again.");
        
        UnitOfWork.Roles.Remove(role);

        await UnitOfWork.CommitAsync(cancellationToken);

        return new DeleteRoleResponse(request.Id);
    }
}