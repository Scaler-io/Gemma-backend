using System.ComponentModel.DataAnnotations;

namespace Gemma.Catalog.API.Models
{
    public class ProductRequest
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageLink { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
