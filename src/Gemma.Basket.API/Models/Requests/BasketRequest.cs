using System.ComponentModel.DataAnnotations;

namespace Gemma.Basket.API.Models.Requests
{
    public class BasketRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserName { get; set; }
        [Required]
        public IEnumerable<ShoppingCartItemRequest> Items { get; set; } = new List<ShoppingCartItemRequest>();
    }
}
