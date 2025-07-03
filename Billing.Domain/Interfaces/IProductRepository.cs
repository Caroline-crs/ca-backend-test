using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsAsync();
    Task AddAsync(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task SaveChangesAsync();
}
