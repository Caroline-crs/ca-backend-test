
using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface ICustomerRepository : IRespository<Customer>
{
    Task<Customer> GetByEmailAsync(string email);

}
