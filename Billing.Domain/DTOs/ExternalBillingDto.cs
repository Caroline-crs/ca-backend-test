namespace Billing.Domain.DTOs;

public record ExternalBillingDto(Guid id, Guid CustomerId, Guid ProductId, decimal Price, int Quantity, DateTime CreatedAt);
