namespace Billing.Domain.DTOs;

public record BillingInformationImportDto(Guid CustomerId, Guid ProductId, decimal Price, int Quantity);
