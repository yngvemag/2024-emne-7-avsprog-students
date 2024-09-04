using PersonRestAPI.Models;

namespace PersonRestAPI.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<Person?> AddAsync(Person person);
    Task<ICollection<Person>> GetAllAsync();
    Task<Person?> DeleteByIdAsync(int id);

    Task<Person?> GetByIdAsync(int id);
    
    Task<Person?> UpdateAsync(int id, Person person);
}