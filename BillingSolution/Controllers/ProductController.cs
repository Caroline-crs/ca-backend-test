using Billing.Application.Services;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Billing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly  IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createProduct = await _productService.CreateProductAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpGet]
	public async Task<IActionResult> GetAllProducts()
	{
		try
		{
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
		try
		{
			var product = await _productService.GetProductByIdAsync(id);
			return Ok(product);
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest();

        await _productService.UpdateProductAsync(id, product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);

        return NoContent();
    }
}
