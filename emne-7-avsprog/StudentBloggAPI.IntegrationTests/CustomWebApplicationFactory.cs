using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public CustomWebApplicationFactory()
    {
        UserRepositoryMock = new Mock<IUserRepository>();    
    }
    
    public Mock<IUserRepository> UserRepositoryMock { get; set; }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton(UserRepositoryMock.Object);
        });
    }
}