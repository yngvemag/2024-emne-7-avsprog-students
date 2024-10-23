using System.Net;
using Newtonsoft.Json;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.IntegrationTests.Features.Users;

public class UsersIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UsersIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    // ...api/v1/users/
    [Fact]
    public async Task GetUsers_WhenNoSearchParams_ThenReturnsPagedUsers()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("/api/v1/users");
        
        // Assert
        var data = JsonConvert
            .DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(data);
    }
}