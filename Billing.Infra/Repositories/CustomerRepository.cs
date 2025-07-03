using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BillingDbContext _context;

    public CustomerRepository(BillingDbContext context)
        => _context = context;

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer>> GetAllCustomersAsync() => await _context.Customers.ToListAsync();

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
        => await _context.Customers.FindAsync(id);

    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
