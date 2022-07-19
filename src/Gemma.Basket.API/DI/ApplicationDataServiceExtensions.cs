using Gemma.Basket.API.DataAccess;
using Gemma.Basket.API.DataAccess.Interface;

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
