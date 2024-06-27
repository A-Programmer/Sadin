using Sadin.Common;

namespace Sadin.Api.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
    public static (WebApplicationBuilder builder,
    PublicSettings _settings) AddBasicConfigurations(this WebApplicationBuilder builder)
    {
        PublicSettings _settings = new();

        IConfiguration Configuration = builder.Environment.IsProduction()
            ? new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            : new ConfigurationBuilder()
                .AddUserSecrets(AssemblyReference.Assembly)
                .AddJsonFile("appsettings.Development.json")
                .Build();

        builder.WebHost.UseUrls("https://localhost:6001;http://localhost:6000");

        Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);
        
        
        
        return (builder, _settings);
    }
    
    public static WebApplication RegisterGeneralPipelines(this WebApplication app)
    {   
        
        // app.UseHttpsRedirection();
        return app;
    }
}