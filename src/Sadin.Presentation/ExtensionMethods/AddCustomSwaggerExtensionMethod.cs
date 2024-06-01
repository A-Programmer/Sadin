using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Sadin.Presentation.ExtensionMethods;

public static class AddCustomSwaggerExtensionMethod
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(SwaggerGroupLabels.General, new OpenApiInfo
            {
                Version = "v1",
                Title = "General Endpoints",
                Description = "Sadin.DEV Backend API system",
                TermsOfService = new Uri("https://sadin.dev/TOS")
            });
            
            options.SwaggerDoc(SwaggerGroupLabels.Admin, new OpenApiInfo
            {
                Version = "v1",
                Title = "Admin Area Endpoints",
                Description = "Sadin.DEV Backend API system",
                TermsOfService = new Uri("https://sadin.dev/TOS")
            });
            
            options.SwaggerDoc(SwaggerGroupLabels.Blog, new OpenApiInfo
            {
                Version = "v1",
                Title = "Blog Endpoints",
                Description = "Sadin.DEV Backend API system",
                TermsOfService = new Uri("https://sadin.dev/TOS")
            });

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                switch (docName)
                {
                    case SwaggerGroupLabels.Admin:
                    {
                        return apiDesc.GroupName == SwaggerGroupLabels.Admin;
                    }
                    case SwaggerGroupLabels.General:
                    {
                        return apiDesc.GroupName == SwaggerGroupLabels.General;
                    }
                    case SwaggerGroupLabels.Blog:
                    {
                        return apiDesc.GroupName == SwaggerGroupLabels.Blog;
                    }
                    default:
                    {
                        return false;
                    }
                }
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }

                    }, Array.Empty<string>()
                }
            });

        });

        return services;
    }

    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{SwaggerGroupLabels.General}/swagger.json", "General Endpoints");
            c.SwaggerEndpoint($"/swagger/{SwaggerGroupLabels.Admin}/swagger.json", "Admin Endpoints");
            c.SwaggerEndpoint($"/swagger/{SwaggerGroupLabels.Blog}/swagger.json", "Blog Endpoints");
            c.RoutePrefix = string.Empty;
        });

        return app;
    }
}