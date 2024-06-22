using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Infrastructure.ExtensionMethods;

namespace Sadin.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomDbContext(configuration);
        services.AddDomainEventRequirements();
        services.AddRepositories();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.ImplementAutoMigrationFeature();
        return app;
    }
}