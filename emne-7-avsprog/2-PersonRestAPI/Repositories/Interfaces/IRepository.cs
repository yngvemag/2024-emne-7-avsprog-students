using System.Formats.Asn1;

namespace PersonRestAPI.Repositories.Interfaces;

public interface IRepository<T> 
    where T : class
{
    Task<T?> AddAsync(T item);
    Task<T?> GetByIdAsync(int id);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> UpdateAsync(T item);
    Task<T?> RemoveAsync(T item);
    Task<T?> RemoveByIdAsync(int id);
}