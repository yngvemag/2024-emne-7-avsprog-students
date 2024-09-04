using MySql.Data.MySqlClient;
using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Repositories;

public class PersonMySqlDatabase(IConfiguration configuration) : IPersonRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task<Person?> AddAsync(Person person)
    {
        await using MySqlConnection conn = new(_connectionString);
        conn.Open();

        var query = "INSERT INTO Person (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)";
        MySqlCommand cmd = new(query, conn);
        cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
        cmd.Parameters.AddWithValue("@LastName", person.LastName);
        cmd.Parameters.AddWithValue("@Age", person.Age);

        await cmd.ExecuteNonQueryAsync();

        // Henter ut siste innlagte id!
        cmd.CommandText = "SELECT LAST_INSERT_ID()";
        person.Id = Convert.ToInt32(cmd.ExecuteScalar());

        return person;
    }

    public async Task<ICollection<Person>> GetAllAsync()
    {
        var personList = new List<Person>();
        await using MySqlConnection conn = new(_connectionString);
        conn.Open();

        var query = "SELECT * FROM Person";
        MySqlCommand cmd = new(query, conn);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var person = new Person()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Age = reader.GetInt32(3)
            };
            personList.Add(person);
        }
        return personList;
    }

    public async Task<Person?> DeleteByIdAsync(int id)
    {
        var personToDelete = await GetByIdAsync(id);
        await using MySqlConnection conn = new(_connectionString);
        conn.Open();

        var query = "DELETE FROM Person where Id = @Id";
        MySqlCommand cmd = new(query, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int rowAffectedCount = await cmd.ExecuteNonQueryAsync();

        if (rowAffectedCount > 0)
            return personToDelete;

        return null;
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        await using MySqlConnection conn = new(_connectionString);
        conn.Open();

        var query = "SELECT Id, FirstName, LastName, Age FROM Person where id = @Id";
        MySqlCommand cmd = new(query, conn);
        cmd.Parameters.AddWithValue("Id", id);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            return new Person()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Age = reader.GetInt32(3)
            };
        }

        return null;
    }

    public async Task<Person?> UpdateAsync(int id, Person person)
    {
        await using MySqlConnection conn = new(_connectionString);
        conn.Open();

        var query = "UPDATE Person SET FirstName=@FirstName, LastName=@LastName, Age=@Age WHERE Id=@Id";
        MySqlCommand cmd = new(query, conn);

        cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
        cmd.Parameters.AddWithValue("@LastName", person.LastName);
        cmd.Parameters.AddWithValue("@Age", person.Age);
        cmd.Parameters.AddWithValue("@Id", person.Id);

        var rowEffectedCount = await cmd.ExecuteNonQueryAsync();
        if (rowEffectedCount == 0)
            return null;

        return await GetByIdAsync(id);

    }
}