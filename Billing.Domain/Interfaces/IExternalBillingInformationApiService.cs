using Billing.Domain.DTOs;

namespace Billing.Domain.Interfaces;

public interface IExternalBillingInformationApiService
{
    Task<List<ExternalBillingDto>> GetAllExternalBillingInformationAsync();
}
