namespace Sadin.Application.Roles.DeleteRole;

public record DeleteRoleCommand(Guid Id) : ICommand<DeleteRoleResponse>;