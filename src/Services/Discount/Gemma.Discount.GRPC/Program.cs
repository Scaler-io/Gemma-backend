using Gemma.Discount.GRPC.DI;
using Gemma.Discount.GRPC.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
var config = builder.Configuration;

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
