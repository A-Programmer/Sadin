using Microsoft.EntityFrameworkCore;
using Sadin.Common.Primitives;
using Sadin.Common.Utilities;

namespace Sadin.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = Domain.AssemblyReference.Assembly;

        #region Register All Entities
        modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        #endregion

        #region Apply Entities Configuration
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        #endregion

        #region Config Delete Behevior for not Cascade Delete
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        #endregion

        #region Add Sequential GUID for Id properties
        modelBuilder.AddSequentialGuidForIdConvention();
        #endregion

        #region Pluralize Table Names
        modelBuilder.AddPluralizingTableNameConvention();
        #endregion
    }
}