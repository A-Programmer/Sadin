using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Application.ExtensionMethods;
using Sadin.Application.Services;
using Sadin.Common.MediatRCommon.Behaviours;

namespace Sadin.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddServices();
        services.AddMediatRService();
        
        return services;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        return app;
    }
}