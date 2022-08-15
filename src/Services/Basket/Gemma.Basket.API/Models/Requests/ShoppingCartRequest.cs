namespace Gemma.Basket.API.Models.Requests
{
    public class ShoppingCartRequest
    {
        public string UserName { get; set; }
        public IEnumerable<ShoppingCartItemRequest> Items { get; set; }
    }
}
