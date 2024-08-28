using PersonRestAPI.Endpoints;
using PersonRestAPI.Repositories;
using PersonRestAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// vi legger inn i container er: Klasse som implementerer IPersonRepository
builder.Services.AddSingleton<IPersonRepository, PersonInMemoryDataStorage>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Lager vårt første endepunkt! Metode: GET, https://localhost:7078/persons/
app.MapPersonEndpoints();

app.Run();

