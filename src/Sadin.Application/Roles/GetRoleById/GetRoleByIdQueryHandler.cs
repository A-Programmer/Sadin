using Sadin.Common.CustomExceptions;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.GetRoleById;

public sealed class GetRoleByIdQueryHandler : CqrsBase, IQueryHandler<GetRoleByIdQuery, RoleItemResponse>
{
    public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
    
    public async Task<RoleItemResponse> Handle(GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        Role? role = await UnitOfWork.Roles.GetByIdAsync(request.Id,
            cancellationToken);

        if (role is null)
            throw new KsNotFoundException(request.Id.ToString());

        return new RoleItemResponse(role.Id, role.Name, role.Description);
    }
}