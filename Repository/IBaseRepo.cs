using API.Models.Pagination;
using System.Linq.Expressions;

namespace API.Repository
{
    public interface IBaseRepo<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<PaginationData<T>> GetAllByPaginationAsync(Expression<Func<T, bool>> filter, Expression<Func<T, dynamic>> orderBy, int pageNo, int pageSize, string? includeProperties = null);
    }
}
