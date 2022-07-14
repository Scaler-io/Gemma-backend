using Gemma.Catalog.API.Models;
using Gemma.Shared.Common;

namespace Gemma.Catalog.API.Services.Product
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Entities.Product>>> GetProducts();
        Task<Result<Entities.Product>> GetProduct(string id);
        Task<Result<IEnumerable<Entities.Product>>> GetProductsByName(string name);
        Task<Result<IEnumerable<Entities.Product>>> GetProductsByCategory(string category);

        Task CreateProduct(ProductRequest request);
        Task<Result<bool>> UpdateProduct(ProductRequest request);
        Task<Result<bool>> DeleteProduct(string id);
    }
}
