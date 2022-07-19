using System.ComponentModel.DataAnnotations;

namespace Gemma.Basket.API.Models.Requests
{
    public class ShoppingCartItemRequest
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
}
