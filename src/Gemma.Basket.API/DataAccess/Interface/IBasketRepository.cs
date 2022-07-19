using Gemma.Basket.API.Entities;

namespace Gemma.Basket.API.DataAccess.Interface
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string username);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string username);
    }
}
