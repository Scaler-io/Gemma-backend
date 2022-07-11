using Gemma.Catalog.API.DI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var config = builder.Configuration;

// Add services to the container.

builder.Services.AddApplicationServices(config)
                .AddDataServices()
                .AddBusinessLayerServices();

var app = builder.Build();

app.AddApplicationMiddlewares();

try
{
    await app.RunAsync();
}
finally
{
    Log.CloseAndFlush();
}
