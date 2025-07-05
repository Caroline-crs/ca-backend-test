using Billing.Domain.DTOs;

namespace Billing.Domain.Interfaces;

public interface IExternalBillingInformationApiService
{
    Task<List<ExternalBillingDto>> GetAllExternalBillingInformationAsync();
    Task<ExternalBillingDto> GetExternalBillingByIdAsync(Guid id);
    Task<ExternalBillingDto> CreateExternalBillingAsync(ExternalBillingDto dto);
    Task<bool> UpdateExternalBillingAsync(Guid id, ExternalBillingDto dto);
    Task<bool> DeleteExternalBillingAsync(Guid id);
}
