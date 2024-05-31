using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.GetPaginatedRoles;

public sealed class GetPaginatedRolesQueryHandler
    : IQueryHandler<GetPaginatedRolesQuery,
        PaginatedList<RolesListItemResponse>>
{
    private readonly IUnitOfWork _uow;

    public GetPaginatedRolesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<PaginatedList<RolesListItemResponse>> Handle(GetPaginatedRolesQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<Role> roles = await _uow.Roles.GetPagedAsync(request.PageIndex,
            request.PageSize,
            request.Where,
            request.OrderByPropertyName,
            request.Desc,
            cancellationToken);
        
        return new PaginatedList<RolesListItemResponse>(roles
                .Select(r => new RolesListItemResponse(r.Id, r.Name, r.Description))
                .ToList(),
            roles.Count,
            roles.PageIndex,
            request.PageSize);
    }
}