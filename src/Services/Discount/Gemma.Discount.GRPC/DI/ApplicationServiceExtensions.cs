using Gemma.Infrastructure;
using Serilog;
using System.Reflection;

namespace Gemma.Discount.GRPC.DI
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var logger = LoggerConfig.Configure(config);
            services.AddSingleton(Log.Logger);
            services.AddSingleton(x => logger);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddGrpc();
            return services;
        }

        public static WebApplication AddApplicationMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            return app;
        }
    }
}
