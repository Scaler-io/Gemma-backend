using Gemma.Catalog.API.DataAccess.Interfaces;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using ILogger = Serilog.ILogger;

namespace Gemma.Catalog.API.Services.Product
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductService(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Entities.Product>>> GetProducts()
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.GetProducts();

            if(result == null)
            {
                _logger.Here().Information($"No products found. {ErrorMessages.NotFound}");
                return Result<IEnumerable<Entities.Product>>.Fail(ErrorCodes.NotFound, "No product found");
            }

            _logger.Here().MethodExited();
            return Result<IEnumerable<Entities.Product>>.Success(result);
        }
        public async Task<Result<Entities.Product>> GetProduct(string id)
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.GetProduct(id);

            if (result == null)
            {
                _logger.Here().Information($"No products found with {id}. {ErrorMessages.NotFound}");
                return Result<Entities.Product>.Fail(ErrorCodes.NotFound, "No product found");
            }

            _logger.Here().ForContext("productId", id).Information($"Product found with id {id}");
            _logger.Here().MethodExited();
            return Result<Entities.Product>.Success(result);
        }

        public async Task<Result<IEnumerable<Entities.Product>>> GetProductsByName(string name)
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.GetProductsByName(name);

            if (result == null)
            {
                _logger.Here().Information($"No products found with name {name}. {ErrorMessages.NotFound}");
                return Result<IEnumerable<Entities.Product>>.Fail(ErrorCodes.NotFound, "No product found");
            }

            _logger.Here().MethodExited();
            return Result<IEnumerable<Entities.Product>>.Success(result);
        }

        public async Task<Result<IEnumerable<Entities.Product>>> GetProductsByCategory(string category)
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.GetProductBycategory(category);

            if (result == null)
            {
                _logger.Here().Information($"No products found with category {category}. {ErrorMessages.NotFound}");
                return Result<IEnumerable<Entities.Product>>.Fail(ErrorCodes.NotFound, "No product found");
            }

            _logger.Here().MethodExited();
            return Result<IEnumerable<Entities.Product>>.Success(result);
        }

        public async Task CreateProduct(Entities.Product product)
        {
            _logger.Here().MethodEnterd();
            try
            {
                await _productRepository.CreateProduct(product);
                _logger.Here().Information("Product created. {@product}", product);
            }
            catch (Exception ex)
            {
                _logger.Here().Error("Failed to create a new product. {@errorMessage}", ErrorMessages.Operationfailed);
            }

            _logger.Here().MethodExited();
        }
        
        public async Task<Result<bool>> UpdateProduct(Entities.Product product)
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.UpdateProduct(product);

            if (!result)
            {
                _logger.Here().Error("Failed to update product with id {@productId}", product.Id);
                return Result<bool>.Fail("Failed to update product. {@errorMessage}", ErrorMessages.Operationfailed);
            }
            _logger.Here().MethodExited();
            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> DeleteProduct(string id)
        {
            _logger.Here().MethodEnterd();
            var result = await _productRepository.DeleteProduct(id);

            if (!result)
            {
                _logger.Here().Error("Failed to delete product with id {@productId}", id);
                return Result<bool>.Fail("Failed to delete product. {@errorMessage}", ErrorMessages.Operationfailed);
            }
            _logger.Here().MethodExited();
            return Result<bool>.Success(true);
        }
    }
}
