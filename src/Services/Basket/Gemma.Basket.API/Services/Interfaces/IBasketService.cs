using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Shared.Common;

namespace Gemma.Basket.API.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Result<ShoppingCartResponse>> GetBasket(string username);
        Task<Result<ShoppingCartResponse>> UpdateBasket(ShoppingCartRequest request);
        Task DeleteBasket(string username);
    }
}
