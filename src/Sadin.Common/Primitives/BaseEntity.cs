
namespace Sadin.Common.Primitives;

public abstract class BaseEntity<TKey>
{
    protected BaseEntity(TKey id) => Id = id;
    public TKey Id { get; init; }

    protected BaseEntity()
    {
        
    }
}

public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}