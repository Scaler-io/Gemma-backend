using Gemma.Basket.API.DataAccess;

namespace Gemma.Basket.API.DI
{
    public static class ApplicationDataServiceExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
