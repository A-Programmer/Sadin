namespace Sadin.Application.Users.GetUserById;

public record UserResponse(Guid Id,
    string UserName,
    string Email,
    string PhoneNumber,
    List<string> Roles);