
using ProductManager.DAL.Entities;

namespace ProductManager.BLL.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize);
    Task<Product?> GetProductByIdAsync(int id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}


