using System.Linq.Expressions;
using Sadin.Common.Interfaces;
using Sadin.Common.Utilities;

namespace Sadin.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    ValueTask<TEntity> GetByIdAsync(object id,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>>? where = null,
        string orderBy = "",
        bool desc = false,
        CancellationToken cancellationToken = default);
    
    PaginatedList<TEntity> GetPaged(int pageIndex,
        int pageSize, Expression<Func<TEntity, bool>>? where = null,
        string orderBy = "",
        bool desc = false);
    
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity,
        CancellationToken cancellationToken = default);
    
    Task AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    void Remove(TEntity entity);
    
    void RemoveRange(IEnumerable<TEntity> entities);
    
    Task<bool> IsExistValueForPropertyAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}