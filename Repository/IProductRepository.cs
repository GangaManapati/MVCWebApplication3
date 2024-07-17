using MVCWebApplication3.Models;

namespace MVCWebApplication3.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
        Task DeactivateProductAsync(int id);
        Task ActivateProductAsync(int id);
        Task<List<Product>> GetAllActivateProductAsync(int pageNumber, int pageSize);
        Task<List<Product>> GetAllDeactiveProAsync(int pageNumber, int pageSize);

    }
}
