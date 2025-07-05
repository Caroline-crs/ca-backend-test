using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IBillingRepository
{

    Task<BillingInformation> GetByIdAsync(Guid id);
    Task AddAsync(BillingInformation billing);
    Task<List<BillingInformation>> GetByCustomerIdAsync(Guid customerId);

    Task<bool> UpdateAsync(BillingInformation id);
    Task<bool> DeleteAsync(Guid id);
}
