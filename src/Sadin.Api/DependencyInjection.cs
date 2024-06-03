using Sadin.Api.ExtensionMethods;
using Sadin.Common;

namespace Sadin.Api;

public static class DependencyInjection
{
    public static WebApplicationBuilder RegisterApi(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.Configure<PublicSettings>(
            configuration.GetSection(nameof(PublicSettings)));
        
        builder.AddSerilog();
        
        return builder;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.RegisterGeneralPipelines();
        return app;
    }
}