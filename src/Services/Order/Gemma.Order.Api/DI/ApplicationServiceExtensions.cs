using EventBus.Message.Common.Constants;
using Gemma.Infrastructure;
using Gemma.Order.Api.Middlewares;
using Gemma.Order.Application.EventConsumers;
using Gemma.Shared.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Gemma.Order.Api.DI
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var logger = LoggerConfig.Configure(config);
            services.AddSingleton(Log.Logger);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            }).AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = HandleFrameorkValidationFailure();
            });

            services.AddSingleton(x => logger);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMassTransit(option => {
                option.AddConsumer<BasketCheckoutConsumer>();
                option.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(config["RabbitmqSettings:ConnectionString"]);
                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, ep =>
                    {
                        ep.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            return services;
        }

        private static Func<ActionContext, IActionResult> HandleFrameorkValidationFailure()
        {
            return actionContext =>
            {
                var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).ToList();

                var validationError = new ApiValidationResponse()
                {
                    Errors = new List<FieldLevelError>()
                };

                foreach (var error in errors)
                {
                    var fieldLevelError = new FieldLevelError()
                    {
                        Code = "Invalid",
                        Field = error.Key,
                        Message = error.Value?.Errors?.First().ErrorMessage
                    };

                    validationError.Errors.Add(fieldLevelError);
                }

                return new UnprocessableEntityObjectResult(validationError);
            };
        }

        public static WebApplication AddApplicationPipelines(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<RequestExceptionMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
