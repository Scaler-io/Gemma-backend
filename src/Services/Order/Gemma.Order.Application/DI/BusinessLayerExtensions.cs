using FluentValidation;
using Gemma.Order.Application.EventConsumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gemma.Order.Application.DI
{
    public static class BusinessLayerExtensions
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<BasketCheckoutConsumer>();
            return services;
        }
    }
}
