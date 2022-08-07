using System.Linq.Expressions;

namespace AdessoRideShare.Repository
{
    public interface ITripRepository<T>
    {
        Task<bool> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<List<T>> SearchByAsync(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes);
        Task<T> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<bool> UpdateAsync(T entity);
        Task<bool> Any(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(object id);
    }
}