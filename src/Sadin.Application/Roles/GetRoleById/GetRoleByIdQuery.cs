namespace Sadin.Application.Roles.GetRoleById;

public record GetRoleByIdQuery(Guid Id) : IQuery<RoleItemResponse>;