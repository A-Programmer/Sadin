using Microsoft.EntityFrameworkCore;
using Sadin.Domain.Aggregates.Roles;
using Sadin.Infrastructure.Data;

namespace Sadin.Infrastructure.Repositories;

public class RolesRepository : Repository<Role>, IRolesRepository
{
    public RolesRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<Role?> GetByRoleName(string roleName,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .FirstOrDefaultAsync(x => x.Name.ToLower() == roleName,
                cancellationToken);
    }

    public async Task<Role?> GetRoleByIdIncludingUsersAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == id,
                cancellationToken);
    }

    public async Task<List<Role>> GetRolesByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Where(r => ids.Contains(r.Id))
            .ToListAsync(cancellationToken);
    }
}