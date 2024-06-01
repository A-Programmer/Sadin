using Microsoft.AspNetCore.Builder;
using Sadin.Common;
using Sadin.Presentation.ExtensionMethods;
using Sadin.Presentation.Handlers;

namespace Sadin.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services,
        PublicSettings settings)
    {
        services.AddGlobalExceptionHandling();
        services.AddCustomControllers();
        services.AddCustomSwagger();
        services.AddCustomAuthentication(settings.JwtOptions);
        
        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseCustomSwagger();
     
        app.UseStatusCodePages();
        app.UseExceptionHandler();
        
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }
}