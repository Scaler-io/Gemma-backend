using Gemma.Order.Application.Contracts.Infrastructure;
using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Application.Models;
using Gemma.Order.Infrastructure.Mail;
using Gemma.Order.Infrastructure.Persistance;
using Gemma.Order.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gemma.Order.Infrastructure.DI
{
    public static class InfrastructureLayerExtensions
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
