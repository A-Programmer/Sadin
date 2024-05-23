using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sadin.Common;
using Sadin.Presentation.ExtensionMethods;

namespace Sadin.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services,
        PublicSettings settings)
    {
        services.AddCustomControllers();
        services.AddCustomSwagger();
        services.AddCustomAuthentication(settings.JwtOptions);
        
        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        return app;
    }
}