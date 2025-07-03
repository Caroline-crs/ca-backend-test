using Billing.Domain.Entities;
using Billing.Domain.Interfaces;

namespace Billing.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository) 
    { 
        _customerRepository = customerRepository; 
    }


    public async Task<Customer> GetCustomerByIdAsync(Guid id)
        => await _customerRepository.GetCustomerByIdAsync(id);

    public async Task<List<Customer>> GetAllCustomersAsync()
        => await _customerRepository.GetAllCustomersAsync();

    public async Task<Customer> CreateCustomerAsync(Customer customer)
        => await _customerRepository.AddCustomerAsync(customer);
    public async Task UpdateCustomerAsync(Guid id, Customer customer)
        => await _customerRepository.UpdateCustomerAsync(customer);

    public async Task DeleteCustomerAsync(Guid id)
        => await _customerRepository.DeleteCustomerAsync(id);
}
