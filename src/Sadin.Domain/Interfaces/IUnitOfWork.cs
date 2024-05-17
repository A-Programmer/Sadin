using Sadin.Domain.Aggregates.Roles;
using Sadin.Domain.Aggregates.Users;

namespace Sadin.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IUsersRepository Users { get; }
    public IRolesRepository Roles { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}