using Gemma.Catalog.API.Entities;
using Gemma.Catalog.API.Models;
using Gemma.Catalog.API.Services.Product;
using Gemma.Shared.Common;
using Gemma.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ILogger = Serilog.ILogger;

namespace Gemma.Catalog.API.Controllers.v1
{
    [ApiVersion("1")]
    public class CatalogController : BaseApiController
    {
        private readonly IProductService _productService;
        public CatalogController(ILogger logger, IProductService productService)
            : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.GetProducts();

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProduct([FromRoute] string id)
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.GetProduct(id);

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpGet("[action]/{name}", Name = "GetProductsByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductsByName([FromRoute] string name)
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.GetProductsByName(name);

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpGet("[action]/{category}", Name = "GetProductsByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] string category)
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.GetProductsByCategory(category);

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            Logger.Here().MethodEnterd();

            await _productService.CreateProduct(request);

            Logger.Here().MethodExited();
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest request)
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.UpdateProduct(request);

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            Logger.Here().MethodEnterd();

            var result = await _productService.DeleteProduct(id);

            Logger.Here().MethodExited();
            return OkOrFail(result);
        }
    }
}
