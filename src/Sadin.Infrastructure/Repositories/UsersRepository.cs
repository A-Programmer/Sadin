using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Users;
using Sadin.Infrastructure.Data;

namespace Sadin.Infrastructure.Repositories;

public sealed class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> FindUserWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> FindUserByUserNameAsync(string userName,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.UserName.ToLower() == userName, cancellationToken);
    }

    public async Task<User?> FindUserByEmailAsync(string email,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email, cancellationToken);
    }

    public async Task<User?> FindUserByPhoneNumber(string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == phoneNumber, cancellationToken);
    }

    public async Task<bool> IsUserNameInUseAsync(Guid id,
        string userName,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .FirstOrDefaultAsync(x => x.Id != id &&
                                      x.UserName.ToLower() == userName,
                cancellationToken) is not null;
    }

    public async Task<bool> IsEmailInUseAsync(Guid id,
        string email,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .FirstOrDefaultAsync(x => x.Id != id &&
                                      x.Email.ToLower() == email,
                cancellationToken) is not null;
    }

    public async Task<bool> IsPhoneNumberInUseAsync(Guid id,
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .FirstOrDefaultAsync(x => x.Id != id &&
                                      x.PhoneNumber.ToLower() == phoneNumber,
                cancellationToken) is not null;
    }

    public async Task<PaginatedList<User>> GetPaginatedUsersWithRolesAsync(int pageIndex,
        int pageSize,
        Expression<Func<User, bool>>? where = null,
        string orderBy = "",
        bool desc = false,
        CancellationToken cancellationToken = default)
    {
        return await PaginatedList<User>
            .CreateAsync(
                Entity.Include(u => u.Roles).AsQueryable(),
                pageIndex,
                pageSize,
                where,
                orderBy,
                desc,
                cancellationToken);
    }
}