using System.ComponentModel.DataAnnotations;

namespace Billing.Domain.DTOs;

public record CustomerDto(string name, [EmailAddress] string Email, string Address);
