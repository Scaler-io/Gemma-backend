using MongoDB.Bson.Serialization.Attributes;

namespace Gemma.Catalog.API.Entities
{
    public class Product: BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
    }
}
