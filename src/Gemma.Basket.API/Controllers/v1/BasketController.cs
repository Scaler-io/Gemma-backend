using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Basket.API.Services.Interface;
using Gemma.Shared.Common;
using Gemma.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ILogger = Serilog.ILogger;

namespace Gemma.Basket.API.Controllers.v1
{
    [ApiVersion("1")]
    public class BasketController: BaseApiController
    {
        private readonly IBasketService _basketService;
        public BasketController(ILogger logger, IBasketService basketService)
            : base(logger)
        {
            _basketService = basketService;
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBasket([FromRoute] string username)
        {
            Logger.Here().MethodEnterd();

            var response = await _basketService.GetBasket(username);

            Logger.Here().MethodExited();

            return OkOrFail(response);
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(BasketResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBasket([FromBody] BasketRequest basketRequest)
        {
            Logger.Here().MethodEnterd();

            var response = await _basketService.UpdateBasket(basketRequest);

            Logger.Here().MethodExited();

            return OkOrFail(response);
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteBasket([FromRoute] string username)
        {
            Logger.Here().MethodEnterd();

            await _basketService.DeleteBasket(username);

            Logger.Here().MethodExited();

            return NoContent();
        }
    }
}
