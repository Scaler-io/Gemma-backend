namespace Gemma.Basket.API.Models.Responses
{
    public class BasketResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int BasketTotal { get; set; }
        public IEnumerable<ShoppingCartItemResponse> Items { get; set; } = new List<ShoppingCartItemResponse>();    
    }
}
