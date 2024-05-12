using Sadin.Api;
using Sadin.Application;
using Sadin.Common;
using Sadin.Domain;
using Sadin.Infrastructure;
using Sadin.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterCommon();
builder.Services.RegisterDomain();
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure();
builder.Services.RegisterPresentation();
builder.Services.RegisterApi();


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

app.UseHttpsRedirection();


app.Run();

