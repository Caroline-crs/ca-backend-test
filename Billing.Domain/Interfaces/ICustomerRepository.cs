
using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface ICustomerRepository 
{
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Guid id);
}
