using Catalog.Api.Data;
using MongoDB.Driver;

namespace Catalog.Api.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            this._context = context;
        }
        public Task CreateProduct(Product product)
        {
      return this._context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
          var result=  await this._context.Products.DeleteOneAsync(p => p.Id == id);
            
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await this._context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductByCategory(string category)
        {
            return await this._context.Products.Find(p => p.Category == category).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
           return await this._context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await this._context.Products.Find(p => p.Name == productName).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
          
           var result=  await this._context.Products.ReplaceOneAsync(filter:p=>p.Id==product.Id,replacement:product);
            return result.IsAcknowledged && result.ModifiedCount>0;
        }
    }
}
