using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Application.Services;
using Sadin.Common.MediatRCommon.Behaviours;

namespace Sadin.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        return app;
    }
}