using Gemma.Catalog.API.Services.Product;

namespace Gemma.Catalog.API.DI
{
    public static class ApplicationBusinessLogicServices
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
