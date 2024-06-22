namespace Sadin.Application.Users.UpdateUserRoles;

public record UpdateUserRolesCommand(Guid Id,
    string[] Roles) : ICommand;