using Billing.Application.Services;
using Billing.Domain.DTOs;
using Billing.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Billing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingsController : ControllerBase
{
    private readonly IBillingService _billingService;
    private readonly IExternalBillingInformationApiService _externalBillingInformationApiService;

    public BillingsController(IBillingService billingService, IExternalBillingInformationApiService externalBillingInformationApiService)
    {
        _billingService = billingService;
        _externalBillingInformationApiService = externalBillingInformationApiService;
    }

    #region Locals Operations

    [HttpGet("{id}")]
    [SwaggerOperation("Obtém um billing local por ID")]
    public async Task<IActionResult> GetBillingById(Guid id)
    {
        var billing = await _billingService.GetBillingByIdAsync(id);
        return billing != null ? Ok(billing) : NotFound();
    }

    [HttpPost("create")]
    [SwaggerOperation("Cria um novo billing local")]
    public async Task<IActionResult> CreateBilling([FromBody] BillingInformationImportDto dto)
    {
        try
        {
            var billing = await _billingService.ImportBillingAsync(dto);
            return CreatedAtAction(nameof(GetBillingById), new { id = billing.Id }, billing);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Atualiza um billing local")]
    public async Task<IActionResult> UpdateBilling(Guid id, [FromBody] BillingInformationImportDto dto)
    {
        try
        {
            var update = await _billingService.UpdateBillingInformationAsync(id, dto);
            return update ? NoContent() : NotFound();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove um billing local")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _billingService.DeleteBillingInformationAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    #endregion

    #region External APIs Operations
    [HttpPost("import-external")]
    [SwaggerOperation("Importa billings da API externa para o banco local")]
    public async Task<IActionResult> ImportExternalBillings()
    {
        try
        {
            var result = await _billingService.ImportExternalBillingsAsync();

            return Ok(new
            {
                ImportedCount = result.ImportedBillingsInformation.Count,
                Errors = result.Errors,
                MissinCustomers = result.Errors.Where(e => e.Contains("Cliente")).Distinct(),
                MissingProducts = result.Errors.Where(e => e.Contains("Produto")).Distinct()
            });
        }
        catch (ApplicationException ex) when (ex.InnerException is HttpRequestException)
        {
            return StatusCode(503, new { Error = "API externa indisponível" });
        }
    }
    
    [HttpGet("external/{id}")]
    [SwaggerOperation("Consulta um billing na API externa (apenas visualização)")]
    public async Task<IActionResult> GetExternalBilling(Guid id)
    {
        try
        {
            var billing = await _externalBillingInformationApiService.GetExternalBillingByIdAsync(id);
            return Ok(billing);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, new { Error = "API externa indisponível" });
        }
    }
    #endregion

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
    
    #region Extras (Relatórios)
    [HttpGet("report/customer/{customerId}")]
    [SwaggerOperation("Gera relatório de faturas por cliente")]
    public async Task<IActionResult> GetCustomerReport(Guid customerId)
    {
        var billings = await _billingService.GetBillingsByCustomerAsync(customerId);
        var total = billings.Sum(b => b.Lines.Sum(l => l.Price * l.Quantity));

        return Ok(new
        {
            CustomerId = customerId,
            TotalInvoiced = total,
            Billings = billings
        });
    }
    #endregion

    //[HttpGet("customer/{customerId}")]
    //public async Task<IActionResult> GetByCustomer(Guid customerId)
    //{
    //    try
    //    {
    //        var billings = await _billingService.GetBillingsByCustomerAsync(customerId);
    //        return Ok(billings);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //}
    //#endregion 
}
