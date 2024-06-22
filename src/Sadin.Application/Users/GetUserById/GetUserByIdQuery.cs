namespace Sadin.Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserResponse>;