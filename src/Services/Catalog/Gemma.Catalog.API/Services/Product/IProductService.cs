using Gemma.Shared.Common;

namespace Gemma.Catalog.API.Services.Product
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Entities.Product>>> GetProducts();
        Task<Result<Entities.Product>> GetProduct(string id);
        Task<Result<IEnumerable<Entities.Product>>> GetProductsByName(string name);
        Task<Result<IEnumerable<Entities.Product>>> GetProductsByCategory(string category);

        Task CreateProduct(Entities.Product product);
        Task<Result<bool>> UpdateProduct(Entities.Product product);
        Task<Result<bool>> DeleteProduct(string id);
    }
}
