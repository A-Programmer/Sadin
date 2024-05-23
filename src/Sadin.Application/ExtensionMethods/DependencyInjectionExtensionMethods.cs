using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Application.Services;
using Sadin.Common.MediatRCommon.Behaviours;

namespace Sadin.Application.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddMediatRService(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}