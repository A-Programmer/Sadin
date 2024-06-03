using Serilog;

namespace Sadin.Api.ExtensionMethods;

public static class SerilogDependencyInjectionExtensionMethods
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return builder;
    }
}