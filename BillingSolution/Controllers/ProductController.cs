using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Billing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly  IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

	[HttpGet]
	public async Task<IActionResult> GetAllProducts()
	{
		try
		{
			var products = _productService.GetAllProductsAsync();
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
}
