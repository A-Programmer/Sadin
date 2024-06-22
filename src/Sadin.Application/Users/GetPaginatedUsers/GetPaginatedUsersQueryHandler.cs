using Sadin.Common.Utilities;

namespace Sadin.Application.Users.GetPaginatedUsers;

public sealed class GetPaginatedUsersQueryHandler
    : IQueryHandler<GetPaginatedUsersQuery,
    PaginatedList<UsersListItemResponse>>
{
    private readonly IUnitOfWork _uow;

    public GetPaginatedUsersQueryHandler(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    public async Task<PaginatedList<UsersListItemResponse>> Handle(
        GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        PaginatedList<User> users = await _uow.Users.GetPaginatedUsersWithRolesAsync(request.PageIndex,
            request.PageSize,
            request.Where,
            request.OrderByPropertyName,
            request.Desc,
            cancellationToken);
        
        return new PaginatedList<UsersListItemResponse>(users
                .Select(u => new UsersListItemResponse(u.Id, u.UserName, u.Email, u.PhoneNumber, u.Roles.Select(r => r.Name).ToList()))
                .ToList(),
            users.Count,
            users.PageIndex,
            request.PageSize);
    }
}