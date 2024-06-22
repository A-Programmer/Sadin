namespace Sadin.Application.Users.UpdateUser;

public record UpdateUserRequest(string UserName,
    string Email,
    string PhoneNumber,
    string[] Roles);