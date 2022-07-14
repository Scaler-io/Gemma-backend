using Gemma.Catalog.API.DataAccess.Interfaces;
using Gemma.Catalog.API.Entities;
using MongoDB.Driver;
using ILogger = Serilog.ILogger;

namespace Gemma.Catalog.API.DataAccess
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration, ILogger logger)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:Database"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:Collection"));
            CatalogContextSeed.SeedData(Products, logger).Wait();
        }
        public IMongoCollection<Product> Products { get; }
    }
}
