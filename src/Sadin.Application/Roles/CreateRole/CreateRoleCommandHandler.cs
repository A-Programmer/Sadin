using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.CreateRole;

public sealed class CreateRoleCommandHandler : CqrsBase, ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
    
    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        Role? existingRole = await UnitOfWork.Roles.GetByRoleName(request.Name,
            cancellationToken);

        if (existingRole is not null)
            throw new KsRecordExistException("The role with this name exist, please choose another name.");
        
        Role roleToCreate = Role.Create(Guid.NewGuid(), request.Name, request.Description);

        await UnitOfWork.Roles.AddAsync(roleToCreate, cancellationToken);

        await UnitOfWork.CommitAsync(cancellationToken);

        return new CreateRoleResponse(roleToCreate.Id, roleToCreate.Name, roleToCreate.Description);
    }
}