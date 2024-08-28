using Microsoft.Extensions.Diagnostics.HealthChecks;
using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Endpoints;

public static class PersonEndpoints
{
    // MapPersonEndpoints
    public static void MapPersonEndpoints(this WebApplication app)
    {
        app.MapGet("/persons", GetPersons).WithName("GetPersons").WithOpenApi();
        app.MapPost("/persons", AddPerson).WithName("AddPerson").WithOpenApi();
        app.MapDelete("/persons/{id}", DeletePerson).WithName("DeletePerson").WithOpenApi();
    }

    private static IResult DeletePerson(IPersonRepository repo, int id)
    {
        var person = repo.DeleteById(id);
        return person is null
            ? Results.BadRequest($"Did`nt find person with id={id}")
            : Results.Ok(person);
    }

    private static IResult GetPersons(IPersonRepository repo)
    {
        // hente fra databasen !!
        return Results.Ok(repo.GetAll());
    }

    private static IResult AddPerson(IPersonRepository repo ,Person person)
    {
        var p = repo.Add(person);
        return p is null
            ? Results.BadRequest("Fail to add database")
            : Results.Ok(p);
    }
}