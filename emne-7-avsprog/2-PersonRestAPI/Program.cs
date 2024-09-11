using PersonRestAPI.Endpoints;
using PersonRestAPI.Middleware;
using PersonRestAPI.Models;
using PersonRestAPI.Repositories;
using PersonRestAPI.Repositories.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    // vi legger inn i container er: Klasse som implementerer IPersonRepository
    builder.Services
        .AddSingleton<IPersonRepository, PersonMySqlDatabase>()
        .AddSingleton<IRepository<Person>, PersonGenericInMemDB>()
        .AddExceptionHandler<GlobalExceptionHandling>()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/logs-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Registration of middleware
    app
        .UseExceptionHandler(_ => { })
        .UseHttpsRedirection();

    // Lager vårt første endepunkt! Metode: GET, https://localhost:7078/persons/
    app.MapPersonEndpoints();
}

app.Run();

