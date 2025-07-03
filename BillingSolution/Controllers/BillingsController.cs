using Billing.Domain.DTOs;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingsController : ControllerBase
{
    private readonly IBillingService _billingService;

    public BillingsController(IBillingService billingService)
    {
        _billingService = billingService;
    }

    [HttpPost]
    public async Task<IActionResult> ImportExternalBillings()
    {
        try
        {
            var result = await _billingService.ImportExternalBillingsAsync();
            return Ok(new
            {
                Success = true,
                ImportedCount = result.ImportedBillingsInformation.Count,
                Errors = result.Errors
            });
        }
        catch (ApplicationException ex) when (ex.InnerException is HttpRequestException)
        {
            return StatusCode(503, new
            {
                Success = false,
                Message = "External API out temporary."
            });
        }
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import([FromBody] BillingInformationImportDto dto)
    {
        try
        {
            var billing = await _billingService.ImportBillingAsync(dto);
            return Ok(billing);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        try
        {
            var billings = await _billingService.GetBillingsByCustomerAsync(customerId);
            return Ok(billings);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
