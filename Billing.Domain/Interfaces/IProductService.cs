using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Guid id, Product product);
    Task DeleteProductAsync(Guid id);
}
