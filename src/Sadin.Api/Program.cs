using Sadin.Api;
using Sadin.Api.ExtensionMethods;
using Sadin.Application;
using Sadin.Common;
using Sadin.Domain;
using Sadin.Infrastructure;
using Sadin.Presentation;

var mainBuilder = WebApplication.CreateBuilder(args);

(WebApplicationBuilder builder,
    PublicSettings settings) = mainBuilder.AddBasicConfigurations();

builder.RegisterApi(builder.Configuration);
builder.Services.RegisterPresentation(settings);
builder.Services.RegisterInfrastructure(builder.Configuration);
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

