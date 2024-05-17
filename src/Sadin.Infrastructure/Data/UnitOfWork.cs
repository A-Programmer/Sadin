using Sadin.Domain.Aggregates.Roles;
using Sadin.Domain.Aggregates.Users;
using Sadin.Domain.Interfaces;
using Sadin.Infrastructure.Repositories;

namespace Sadin.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }

    private UsersRepository? _users;
    public IUsersRepository Users => _users ??= new UsersRepository(_db);

    private RolesRepository? _roles;
    public IRolesRepository Roles => _roles ??= new RolesRepository(_db);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _db.SaveChangesAsync(cancellationToken);
    }
    public  void Dispose()
    {
        _db.Dispose();
    }
}