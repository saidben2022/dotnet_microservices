namespace Catalog.Api.Models.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetProductByName(string productName);
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByCategory(string category);

        Task CreateProduct(Product product);
         
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);

    }
}
