using Sadin.Common.Primitives;

namespace Sadin.Domain;

public abstract class Entity : BaseEntity<Guid>, IEquatable<Entity>
{
    protected Entity(Guid id) : base(id)
    {
    }

    protected Entity()
    {
    }

    public bool IsDeleted { get; private set; }
    
    /// <summary>
    /// Gets the created on date and time in UTC format.
    /// </summary>
    public DateTimeOffset CreatedOnUtc { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets the created on date and time in UTC format.
    /// </summary>
    public DateTimeOffset? ModifiedOnUtc { get; private set; }

    public void Delete() => IsDeleted = true;
    public void Recover() => IsDeleted = false;

    public void SetCreatorInfo()
    {
        CreatedOnUtc = DateTimeOffset.Now;
    }

    public void SetModifierInfo()
    {
        ModifiedOnUtc = DateTimeOffset.Now;
    }

    public static bool operator ==(Entity? first, Entity? second) =>
        first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity? first, Entity? second) =>
        !(first == second);

    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;
        if (other.GetType() != GetType())
            return false;
        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != GetType())
            return false;
        if (obj is not Entity entity)
            return false;
        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}