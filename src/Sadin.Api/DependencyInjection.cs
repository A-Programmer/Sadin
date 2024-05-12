namespace Sadin.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApi(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        return app;
    }
}