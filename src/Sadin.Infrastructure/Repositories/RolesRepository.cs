using Microsoft.EntityFrameworkCore;
using Sadin.Domain.Aggregates.Roles;
using Sadin.Infrastructure.Data;

namespace Sadin.Infrastructure.Repositories;

public class RolesRepository : Repository<Role>, IRolesRepository
{
    public RolesRepository(DbContext context) : base(context)
    {
    }
    
    public async Task<Role?> GetByRoleName(string roleName,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .FirstOrDefaultAsync(x => x.Name.ToLower() == roleName,
                cancellationToken);
    }
}