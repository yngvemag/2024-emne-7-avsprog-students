using PersonRestAPI.Endpoints;
using PersonRestAPI.Models;
using PersonRestAPI.Repositories;
using PersonRestAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
{
    // vi legger inn i container er: Klasse som implementerer IPersonRepository
    builder.Services
        .AddSingleton<IPersonRepository, PersonMySqlDatabase>()
        .AddSingleton<IRepository<Person>, PersonGenericInMemDB>()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Lager vårt første endepunkt! Metode: GET, https://localhost:7078/persons/
    app.MapPersonEndpoints();
}



app.Run();

