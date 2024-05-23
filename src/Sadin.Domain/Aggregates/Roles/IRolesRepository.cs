using Sadin.Domain.Interfaces;

namespace Sadin.Domain.Aggregates.Roles;

public interface IRolesRepository : IRepository<Role>
{
    Task<Role?> GetByRoleName(string roleName,
        CancellationToken cancellationToken = default);
}