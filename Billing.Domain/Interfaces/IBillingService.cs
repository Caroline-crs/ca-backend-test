using Billing.Domain.DTOs;
using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IBillingService
{
    /// <summary>
    /// Importa uma nova fatura verificando cliente e produto
    /// </summary>
    Task<BillingInformation> ImportBillingAsync(BillingInformationImportDto dto);

    /// <summary>
    /// Obtém uma fatura por ID com suas linhas
    /// </summary>
    Task<BillingInformation> GetBillingByIdAsync(Guid id);

    /// <summary>
    /// Lista faturas por cliente
    /// </summary>
    Task<List<BillingInformation>> GetBillingsByCustomerAsync(Guid customerId);

    /// <summary>
    /// Altera fatura por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> UpdateBillingInformationAsync(Guid id, BillingInformationImportDto dto);

    /// <summary>
    /// Deleta fatura por ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteBillingInformationAsync(Guid id);

    Task<ImportResult> ImportExternalBillingsAsync();

}
