namespace Gemma.Basket.API.Models.Responses
{
    public class BasketResponse
    {
        public string UserName { get; set; }
        public int BasketTotal { get; set; }
        public IEnumerable<ShoppingCartItemResponse> Items { get; set; } = new List<ShoppingCartItemResponse>();
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
