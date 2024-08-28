using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Repositories;

public class PersonInMemoryDataStorage : IPersonRepository
{
    private static readonly List<Person> _dbInMemStorage = [];
    
    public Person? Add(Person person)
    {
        // legge til en liste
        person.id = _dbInMemStorage.Count + 1;
        _dbInMemStorage.Add(person);
        return person;
    }

    public ICollection<Person> GetAll()
    {
        // hente alt fra listen
        return _dbInMemStorage;
    }

    public Person? DeleteById(int id)
    {
        // vi må fjerne riktig person fra listen hvis personen finnes.
        var p = _dbInMemStorage.FirstOrDefault(p => p.id == id);

        if (p is not null)
        {
            _dbInMemStorage.Remove(p);
        }

        return p;

    }

    public Person? Update(int id, Person person)
    {
        var p = _dbInMemStorage.FirstOrDefault(p => p.id == id);
        if (p is null) return null;

        p.FirstName = person.FirstName;
        p.LastName = person.LastName;
        p.Age = person.Age;
        return p;
    }
}