using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Repositories;

public class PersonInMemoryDataStorage : IPersonRepository
{
    private static readonly List<Person> _dbInMemStorage = [];
    
    public async Task<Person?> AddAsync(Person person)
    {
        await Task.Delay(10);
        // legge til en liste
        person.Id = _dbInMemStorage.Count + 1;
        _dbInMemStorage.Add(person);
        return person;
    }

    public async Task<ICollection<Person>> GetAllAsync()
    {
        await Task.Delay(10);
        // hente alt fra listen
        return _dbInMemStorage;
    }

    public async Task<Person?> DeleteByIdAsync(int id)
    {
        await Task.Delay(10);
        // vi må fjerne riktig person fra listen hvis personen finnes.
        var p = _dbInMemStorage.FirstOrDefault(p => p.Id == id);

        if (p is not null)
        {
            _dbInMemStorage.Remove(p);
        }

        return p;

    }

    public Task<Person?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> UpdateAsync(int id, Person person)
    {
        await Task.Delay(10);
        var p = _dbInMemStorage.FirstOrDefault(p => p.Id == id);
        if (p is null) return null;

        p.FirstName = person.FirstName;
        p.LastName = person.LastName;
        p.Age = person.Age;
        return p;
    }
}