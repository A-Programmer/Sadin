using Sadin.Common;

namespace Sadin.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PublicSettings>(
            configuration.GetSection(nameof(PublicSettings)));
        
        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        return app;
    }
}