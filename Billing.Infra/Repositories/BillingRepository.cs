using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Repositories;

public class BillingRepository : IBillingRepository
{
    private readonly BillingDbContext _context;
    public BillingRepository(BillingDbContext context) => _context = context;

    public async Task AddAsync(BillingInformation billing)
    {
        await _context.Billings.AddAsync(billing);
    }

    public async Task<BillingInformation> GetByIdAsync(Guid id)
    {
        return await _context.Billings
        .Include(b => b.Lines)
        .FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task<List<BillingInformation>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _context.Billings
         .Where(b => b.CustomerId == customerId)
         .Include(b => b.Lines)
         .OrderByDescending(b => b.CreatedAt)
         .AsNoTracking()
         .ToListAsync();
    }

    public async Task<bool> UpdateAsync(BillingInformation billing)
    {
        _context.Billings.Update(billing);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var billing = await _context.Billings.FindAsync(id);
        if (billing == null) return false;

        _context.Billings.Remove(billing);
        return await _context.SaveChangesAsync() > 0;
    }
}
