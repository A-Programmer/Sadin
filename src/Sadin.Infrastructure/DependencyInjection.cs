using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Domain.Interfaces;
using Sadin.Infrastructure.Data;

namespace Sadin.Infrastructure;

public static class DependencyInjection
{
    // TODO: Replace IConfiguration with PublicSettings and IOption Pattern
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                x =>
                    x.MigrationsAssembly(Infrastructure.AssemblyReference.Assembly.FullName));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        return app;
    }
}