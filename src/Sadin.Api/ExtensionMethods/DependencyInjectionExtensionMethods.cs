using Sadin.Common;

namespace Sadin.Api.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
    public static (WebApplicationBuilder builder,
    IConfiguration configuration,
    PublicSettings _settings) AddBasicConfigurations(this WebApplicationBuilder builder)
    {
        PublicSettings _settings = new();

        IConfiguration Configuration = builder.Environment.IsProduction()
            ? new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            : new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

        Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);
        
        return (builder, Configuration, _settings);
    }
    
    
}