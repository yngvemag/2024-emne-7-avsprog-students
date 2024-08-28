using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Repositories;

public class PersonGenericInMemDB : IRepository<Person>
{
    public Task<Person?> AddAsync(Person item)
    {
        throw new NotImplementedException();
    }

    public Task<Person?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Person>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Person?> UpdateAsync(Person item)
    {
        throw new NotImplementedException();
    }

    public Task<Person?> RemoveAsync(Person item)
    {
        throw new NotImplementedException();
    }

    public Task<Person?> RemoveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}