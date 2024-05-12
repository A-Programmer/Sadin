using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Sadin.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        return app;
    }
}