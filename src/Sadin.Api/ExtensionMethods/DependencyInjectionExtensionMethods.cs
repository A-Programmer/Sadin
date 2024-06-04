using Sadin.Common;

namespace Sadin.Api.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
    public static (WebApplicationBuilder builder,
    IConfiguration configuration,
    PublicSettings _settings) AddBasicConfigurations(this WebApplicationBuilder builder)
    {
        PublicSettings _settings = new();

        IConfiguration Configuration = new ConfigurationBuilder().Build();

        Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);
        
        return (builder, Configuration, _settings);
    }
    
    public static WebApplication RegisterGeneralPipelines(this WebApplication app)
    {
        return app;
    }
}