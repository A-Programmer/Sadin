using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Sadin.Domain.Interfaces;
using Sadin.Infrastructure.BackgroundJobs;
using Sadin.Infrastructure.Data;
using Sadin.Infrastructure.Interceptors;

namespace Sadin.Infrastructure.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                    x =>
                        x.MigrationsAssembly(Infrastructure.AssemblyReference.Assembly.FullName))
                .AddInterceptors(interceptor);
        });
        
        return services;
    }

    public static IServiceCollection AddDomainEventRequirements(this IServiceCollection services)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
            
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static WebApplication ImplementAutoMigrationFeature(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        
        return app;
    }
}