using PersonRestAPI.Models;

namespace PersonRestAPI.Repositories.Interfaces;

public interface IPersonRepository
{
    Person? Add(Person person);
    ICollection<Person> GetAll();

    Person? DeleteById(int id);
    Person? Update(int id, Person person);
}