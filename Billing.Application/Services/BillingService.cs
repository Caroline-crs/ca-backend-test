using Billing.Domain.DTOs;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;

namespace Billing.Application.Services;

public class BillingService : IBillingService
{
    private readonly IBillingRepository _billingRepository;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly IExternalBillingInformationApiService _externalBillingInformationApiService;

    public BillingService(
        IBillingRepository billingRepository, 
        ICustomerService customerService, 
        IProductService productService,
        IExternalBillingInformationApiService externalBillingInformationApiService)
    {
        _billingRepository = billingRepository;
        _customerService = customerService;
        _productService = productService;
        _externalBillingInformationApiService = externalBillingInformationApiService;
    }

    public async Task<BillingInformation> ImportBillingAsync(BillingInformationImportDto dto)
    {
        var customer = await _customerService.GetCustomerByIdAsync(dto.CustomerId);
        var product = await _productService.GetProductByIdAsync(dto.ProductId);

        var billing = new BillingInformation
        {
            CustomerId = dto.CustomerId,
            Lines = new List<BillingLine>
        {
            new BillingLine
            {
                ProductId = dto.ProductId,
                Price = dto.Price,
                Quantity = dto.Quantity
            }
        }
        };

        await _billingRepository.AddAsync(billing);

        return billing;
    }
    public async Task<List<BillingInformation>> GetBillingsByCustomerAsync(Guid customerId)
    {
        var customerExists = await _customerService.GetCustomerByIdAsync(customerId);
        if (customerExists == null)
            throw new KeyNotFoundException("Cliente não encontrado");

        return await _billingRepository.GetByCustomerIdAsync(customerId);
    }
    public async Task<BillingInformation> GetBillingByIdAsync(Guid id)
    {
        return await _billingRepository.GetByIdAsync(id); 
    }
    public async Task<ImportResult> ImportExternalBillingsAsync()
    {
        var externalBillings = await _externalBillingInformationApiService.GetAllExternalBillingInformationAsync();
        var result = new ImportResult();

        foreach (var externalBilling in externalBillings)
        {
            try
            {
                var billing = await ImportBillingAsync(new BillingInformationImportDto(
                    externalBilling.CustomerId,
                    externalBilling.ProductId,
                    externalBilling.Price,
                    externalBilling.Quantity
                 ));

                result.ImportedBillingsInformation.Add(billing);
            }
            catch (KeyNotFoundException ex)
            {
                result.Errors.Add($"Import failed: {externalBilling.id}: {ex.Message}");
            }
        }
        return result;
    }
}
