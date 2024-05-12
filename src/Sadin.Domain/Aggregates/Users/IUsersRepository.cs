using Sadin.Domain.Interfaces;

namespace Sadin.Domain.Aggregates.Users;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> FindUserWithRoles(Guid id,
        CancellationToken cancellationToken = default);
    
    Task<User?> FindUserByUserName(string userName,
        CancellationToken cancellationToken = default);

    Task<User?> FindUserByEmail(string email,
        CancellationToken cancellationToken = default);

    Task<User?> FindUserByPhoneNumber(string phoneNumber,
        CancellationToken cancellationToken = default);
}