using Microsoft.Extensions.Diagnostics.HealthChecks;
using PersonRestAPI.Models;
using PersonRestAPI.Repositories.Interfaces;

namespace PersonRestAPI.Endpoints;

public static class PersonEndpoints
{
    // MapPersonEndpoints
    public static void MapPersonEndpoints(this WebApplication app)
    {
        var personGroup = app.MapGroup("/persons");
        
        personGroup.MapGet("", GetPersonsAsync).WithName("GetPersons").WithOpenApi();
        personGroup.MapPost("", AddPersonAsync).WithName("AddPerson").WithOpenApi();
        personGroup.MapDelete("/{id}", DeletePersonAsync).WithName("DeletePerson").WithOpenApi();
        personGroup.MapPut("/{id}", UpdatePersonAsync).WithName("UpdatePersonAsync").WithOpenApi();
    }

    private static async Task<IResult> UpdatePersonAsync(IPersonRepository repo, int id, Person person)
    {
        var p = await repo.UpdateAsync(id, person);
        return p is null
            ? Results.BadRequest($"Failed to update person with id={id}")
            : Results.Ok(p);
    }

    private static async Task<IResult> DeletePersonAsync(IPersonRepository repo, int id)
    {
        var person = await repo.DeleteByIdAsync(id);
        return person is null
            ? Results.BadRequest($"Did`nt find person with id={id}")
            : Results.Ok(person);
    }

    private static async Task<IResult> GetPersonsAsync(IPersonRepository repo)
    {
        // hente fra databasen !!
        return Results.Ok(await repo.GetAllAsync());
    }

    private static async Task<IResult> AddPersonAsync(IPersonRepository repo ,Person person)
    {
        var p = await repo.AddAsync(person);
        return p is null
            ? Results.BadRequest("Fail to add database")
            : Results.Ok(p);
    }
}