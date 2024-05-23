using Sadin.Api;
using Sadin.Application;
using Sadin.Common;
using Sadin.Domain;
using Sadin.Infrastructure;
using Sadin.Presentation;

PublicSettings _settings = new();

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Environment.IsProduction()
    ? new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
    : new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json")
        .Build();

Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);

builder.Services.RegisterApi(Configuration);
builder.Services.RegisterPresentation(_settings);
builder.Services.RegisterInfrastructure(Configuration);
builder.Services.RegisterApplication();
builder.Services.RegisterDomain();
builder.Services.RegisterCommon();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCommon();
app.UseDomain();
app.UseApplication();
app.UseInfrastructure();
app.UsePresentation();
app.UseApi();


app.Run();

