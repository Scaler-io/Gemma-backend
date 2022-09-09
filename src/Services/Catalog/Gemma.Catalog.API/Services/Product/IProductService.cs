using Gemma.Catalog.API.Models.Requests;
using Gemma.Catalog.API.Models.Responses;
using Gemma.Shared.Common;

namespace Gemma.Catalog.API.Services.Product
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductResponse>>> GetProducts();
        Task<Result<ProductResponse>> GetProduct(string id);
        Task<Result<IEnumerable<ProductResponse>>> GetProductsByName(string name);
        Task<Result<IEnumerable<ProductResponse>>> GetProductsByCategory(string category);

        Task CreateProduct(ProductRequest request);
        Task<Result<bool>> UpdateProduct(ProductRequest request);
        Task<Result<bool>> DeleteProduct(string id);
    }
}
