namespace Billing.Domain.Entities;

public class ImportResult
{
    public List<BillingInformation> ImportedBillingsInformation { get; set; }
    public List<string> Errors { get; set; } = new();
}
