using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;


public interface ICustomerService
{
    Task<Customer> GetCustomerByIdAsync(Guid id);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Guid id, Customer customer);
    Task DeleteCustomerAsync(Guid id);
}
