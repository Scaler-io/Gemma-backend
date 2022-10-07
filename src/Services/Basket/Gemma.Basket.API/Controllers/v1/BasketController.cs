using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Basket.API.Services.GrpcServices;
using Gemma.Basket.API.Services.Interfaces;
using Gemma.Shared.Common;
using Gemma.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ILogger = Serilog.ILogger;

namespace Gemma.Basket.API.Controllers.v1
{

    [ApiVersion("1")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(ILogger logger, IBasketService basketService, DiscountGrpcService discountGrpcService)
            : base(logger)
        {
            _basketService = basketService;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCartRequest basketRequest)
        {
            Logger.Here().MethodEnterd();

            foreach(var item in basketRequest.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountCoupon(item.ProductId);
                item.Price -= coupon.Amount;
                if (coupon == null)
                {
                    Logger.Here().Error("No coupon for {@productId} - {@productName}", item.ProductId, item.ProductName);
                }
                else
                {
                    Logger.Here().Information("Coupon found {@couponId} for {@productId} - {@productName}", coupon.Id, item.ProductId, item.ProductName);
                    item.Price -= coupon.Amount;
                    Logger.Here().Information("Total ammount deducted {@amount}. Current price is {@currentPrice}", coupon.Amount, item.Price);
                }
            }

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

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiValidationResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Checkout([FromBody] BasketChekoutRequest request)
        {
            Logger.Here().MethodEnterd();
            var result = await _basketService.CheckoutBasketAsync(request);
            Logger.Here().MethodExited();
            return OkOrFail(result);
        }
    }
}
