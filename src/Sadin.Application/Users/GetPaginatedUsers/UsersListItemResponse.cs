namespace Sadin.Application.Users.GetPaginatedUsers;

public record UsersListItemResponse(Guid Id,
    string UserName,
    string Email,
    string PhoneNumber,
    List<string> Roles);