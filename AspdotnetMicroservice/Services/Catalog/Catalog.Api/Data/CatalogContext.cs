using Catalog.Api.Models;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext

    {
        public IMongoCollection<Product> Products { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("ProductDatabase:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("ProductDatabase:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("ProductDatabase:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
      
    }
}
