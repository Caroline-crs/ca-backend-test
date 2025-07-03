namespace Billing.Domain.Configurations;

public class ExternalApiConfig
{
    public const string SectionName = "ExternalApis";
    public string BillingApi { get; set; }
}
