using System.Linq.Expressions;

namespace StudentBloggAPI.Features.Common.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize); // paginering
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate );
}