
namespace Billing.Domain.Entities;

public class BillingInformation 
{ 
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }  
    public List<BillingLine> Lines { get; set; } = new();
}
