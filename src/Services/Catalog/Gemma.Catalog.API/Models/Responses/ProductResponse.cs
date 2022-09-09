namespace Gemma.Catalog.API.Models.Responses
{
    public class ProductResponse
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
        public MetaData MetaData { get; set; }
    }
}
