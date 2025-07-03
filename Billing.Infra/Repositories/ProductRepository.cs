using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BillingDbContext _context;

    public ProductRepository(BillingDbContext context) => _context = context;
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }
    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }
    public void Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
    }
    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    
}
