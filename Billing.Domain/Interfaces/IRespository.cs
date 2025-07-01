
using Billing.Domain.Entities;

namespace Billing.Domain.Interfaces;

public interface IRespository<T> where T : class
{
    Task<T> GetByIAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}


