using Sadin.Api;
using Sadin.Api.ExtensionMethods;
using Sadin.Application;
using Sadin.Common;
using Sadin.Domain;
using Sadin.Infrastructure;
using Sadin.Presentation;

var mainBuilder = WebApplication.CreateBuilder(args);

(WebApplicationBuilder builder,
    IConfiguration Configuration,
    PublicSettings _settings) = mainBuilder.AddBasicConfigurations();

builder.RegisterApi(Configuration);
builder.Services.RegisterPresentation(_settings);
builder.Services.RegisterInfrastructure(Configuration);
builder.Services.RegisterApplication();
builder.Services.RegisterDomain();
builder.Services.RegisterCommon();

var app = builder.Build();

app.UsePresentation();
app.UseCommon();
app.UseDomain();
app.UseApplication();
app.UseInfrastructure();
app.UseApi();


app.Run();

