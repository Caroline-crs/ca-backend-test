using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task Delete(Product product);
}
