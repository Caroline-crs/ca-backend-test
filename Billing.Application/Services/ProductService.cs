using Billing.Domain.Entities;
using Billing.Domain.Interfaces;

namespace Billing.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) => _productRepository = productRepository;
    public async Task<Product> CreateProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        return product;
    }
    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product ?? throw new KeyNotFoundException("Product Not Found.");
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }
    public async Task UpdateProductAsync(Guid id, Product product)
    {
        if (id != product.Id)
            throw new ArgumentException("IDs do not match.");

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
    }
    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        
        if (product == null) return;

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
    }
}
