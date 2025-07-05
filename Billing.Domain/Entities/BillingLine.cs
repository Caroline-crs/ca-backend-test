
using System.Text.Json.Serialization;

namespace Billing.Domain.Entities;

public class BillingLine
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }  
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
