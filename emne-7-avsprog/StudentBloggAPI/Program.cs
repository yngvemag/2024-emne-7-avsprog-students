using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentBloggAPI.Configurations;
using StudentBloggAPI.Data;
using StudentBloggAPI.Extensions;
using StudentBloggAPI.Features.Users;
using StudentBloggAPI.Features.Common.Interfaces;
using StudentBloggAPI.Features.Users.Interfaces;
using StudentBloggAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IMapper<User, UserDTO>, UserMapper>()
    .AddScoped<IMapper<User, UserRegistrationDTO>, UserRegistrationMapper>()
    .AddScoped<IUserRepository, UserRepository>();

builder.Services
    .AddScoped<StudentBloggBasicAuthentication>()
    .Configure<BasicAuthenticationOptions>(builder.Configuration.GetSection("BasicAuthenticationOptions"));

// Add dbcontext
builder.Services.AddDbContext<StudentBloggDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 33))));
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwaggerBasicAuthentication();

// dette er ikke noe dere trenger å huske -> slå opp i serilog og se hvordan dette løses.
builder.Host.UseSerilog((context, configuration) => 
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseMiddleware<StudentBloggBasicAuthentication>()
    .UseAuthorization();

app.MapControllers();

app.Run();