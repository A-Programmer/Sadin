using Microsoft.EntityFrameworkCore;
using Sadin.Domain.Aggregates.Users;
using Sadin.Infrastructure.Data;

namespace Sadin.Infrastructure.Repositories;

public sealed class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(DbContext context) : base(context)
    {
    }

    public async Task<User?> FindUserWithRoles(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> FindUserByUserName(string userName,
        CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.UserName.ToLower() == userName, cancellationToken);
    }

    public async Task<User?> FindUserByEmail(string email,
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
}