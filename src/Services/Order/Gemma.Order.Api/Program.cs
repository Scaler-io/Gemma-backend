using Gemma.Order.Api.DI;
using Gemma.Order.Api.Extensions;
using Gemma.Order.Application.DI;
using Gemma.Order.Infrastructure.DI;
using Gemma.Order.Infrastructure.Persistance;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var config = builder.Configuration;

builder.Services.AddApplicationServices(config)
                .AddBusinessLayerServices()
                .AddInfrastructureLayerServices(config);


var app = builder.Build();

app.AddApplicationPipelines();

try
{
    app.MigrateDatabase<OrderContext>((context, service) =>
    {
        var logger = service.GetRequiredService<ILogger>();
        OrderContextSeed.SeedAsync(context, logger).Wait();
    });
    await app.RunAsync();
}
finally
{
    Log.CloseAndFlush();
}


