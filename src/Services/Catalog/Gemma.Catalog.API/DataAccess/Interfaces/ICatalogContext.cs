using Gemma.Catalog.API.Entities;
using MongoDB.Driver;

namespace Gemma.Catalog.API.DataAccess.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
