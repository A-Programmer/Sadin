using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sadin.Common.Abstractions;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Users;

namespace Sadin.Domain.Aggregates.Roles;

public sealed class Role : AggregateRoot
{
    private Role(Guid id,
        string name,
        string description) : base(id)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (description.HasValue())
            Description = description;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;

    private List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users;

    public void Update(string name,
        string description)
    {
        if (!name.HasValue())
            throw new ArgumentNullException(nameof(name));
        Name = name;

        if (!description.HasValue())
            Description = description;
    }

    public static Role Create(Guid id,
        string name,
        string description)
    {
        Role role = new(id, name, description);

        return role;
    }
}

public class UserConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Users);

        builder.HasData(new List<Role>()
        {
            Role.Create(Guid.NewGuid(),"Admin", "Admin Group"),
            Role.Create(Guid.NewGuid(),"User", "Users Group")
        });
    }
}