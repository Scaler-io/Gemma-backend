namespace Gemma.Basket.API.Models.Responses
{
    public class ShoppingCartResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ShoppingCartItemResponse> Items { get; set; }
        public MetaData MetaData { get; set; }
        public decimal Gross { get; set; }
    }

    public class ShoppingCartItemResponse
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
