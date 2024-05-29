using Sadin.Presentation.Handlers;

namespace Sadin.Presentation.ExtensionMethods;

public static class AddGlobalExceptionHandilgExtensionMethod
{
    public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(opt =>
        {
            opt.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                ctx.ProblemDetails.Extensions.Add("address", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                ctx.ProblemDetails.Extensions.Add("Server-Name", Environment.MachineName);
            };
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}