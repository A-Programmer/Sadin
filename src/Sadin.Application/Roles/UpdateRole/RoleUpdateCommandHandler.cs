using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.UpdateRole;

public sealed class RoleUpdateCommandHandler : CqrsBase, ICommandHandler<RoleUpdateCommand, RoleUpdateResponse>
{
    public RoleUpdateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public async Task<RoleUpdateResponse> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        Role? existingRole = await UnitOfWork.Roles.GetByIdAsync(request.Id, cancellationToken);

        if (existingRole is null)
            throw new KsNotFoundException(request.Id.ToString());
        
        existingRole.Update(request.Name, request.Description);

        await UnitOfWork.CommitAsync(cancellationToken);

        return new RoleUpdateResponse(request.Id, request.Name, request.Description);
    }
}