using Gemma.Discount.GRPC.Repositories;

namespace Gemma.Discount.GRPC.DI
{
    public static class ApplicationDataServiceExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscontRepository>();
            return services;
        }
    }
}
