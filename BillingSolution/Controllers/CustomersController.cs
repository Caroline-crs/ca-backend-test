using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdCustomer = await _customerService.CreateCustomerAsync(customer);

        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customer = await _customerService.GetAllCustomersAsync();
        return Ok(customer);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return customer == null ? NotFound() : Ok(customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
    {
        if (id != customer.Id) return BadRequest();

        await _customerService.UpdateCustomerAsync(id, customer);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await _customerService.DeleteCustomerAsync(id);

        return NoContent();
    }
}

