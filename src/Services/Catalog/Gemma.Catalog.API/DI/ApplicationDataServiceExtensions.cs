using Gemma.Catalog.API.DataAccess;
using Gemma.Catalog.API.DataAccess.Interfaces;

namespace Gemma.Catalog.API.DI
{
    public static class ApplicationDataServiceExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>()
                    .AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
