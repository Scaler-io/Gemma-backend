using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Shared.Common;

namespace Gemma.Basket.API.Services.Interface
{
    public interface IBasketService
    {
        Task<Result<BasketResponse>> GetBasket(string username);
        Task<Result<BasketResponse>> UpdateBasket(BasketRequest request);
        Task DeleteBasket(string username);
    }
}
