using System.Linq.Expressions;

namespace FinancialGoalsManager.Core.Contracts.Repository.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllASync(params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Guid id);

        Task<bool> SaveChangesAsync();

    }
}
