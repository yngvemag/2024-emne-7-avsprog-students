using PersonRestAPI.Models;

namespace PersonRestAPI.Endpoints;

public static class PersonEndpoints
{
    // MapPersonEndpoints
    public static void MapPersonEndpoints(this WebApplication app)
    {
        app.MapGet("/persons", GetPersons).WithName("GetPersons").WithOpenApi();
            
        app.MapPost("/persons", (Person person) =>
            {
                return Results.Ok(new Person()
                {
                    Age = person.Age + 1,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    id = person.id
                });
            }).WithName("AddPersons")
            .WithOpenApi();
    }
    private static IResult GetPersons()
    {
        var person = new Person { Age = 20, id = 1, FirstName = "Ola", LastName = "Normann" };
        return Results.Ok(person);
    }

    private static IResult AddPerson(Person person)
    {
        
    }
}