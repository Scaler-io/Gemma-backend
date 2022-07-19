using System.ComponentModel.DataAnnotations;

namespace Gemma.Basket.API.Models.Requests
{
    public class BasketRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public IEnumerable<ShoppingCartItemRequest> Items { get; set; } = new List<ShoppingCartItemRequest>();
    }
}
