using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BillingDbContext _context;

    public ProductRepository(BillingDbContext context) => _context = context;
    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }
    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }
    public async Task UpdateProductAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task Delete(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }    
}
