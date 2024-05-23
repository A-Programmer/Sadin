using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Sadin.Domain.Interfaces;
using Sadin.Infrastructure.BackgroundJobs;
using Sadin.Infrastructure.Data;
using Sadin.Infrastructure.Interceptors;

namespace Sadin.Infrastructure;

public static class DependencyInjection
{
    // TODO: Replace IConfiguration with PublicSettings and IOption Pattern
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                x =>
                    x.MigrationsAssembly(Infrastructure.AssemblyReference.Assembly.FullName))
                .AddInterceptors(interceptor);
        });
        
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