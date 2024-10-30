using System.Linq.Expressions;
using System.Net;
using Moq;
using Newtonsoft.Json;
using StudentBloggAPI.Features.Users;
using StudentBloggAPI.IntegrationTests.Features.Users.TestData;

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
        List<User> users =
        [
            new()
            {
                FirstName = "Ola", LastName = "Normann", Email = "ola@gmail.com",
                Id = Guid.Parse("3e2385f8-84b6-4e9f-962f-414391a8e7de"),
                UserName = "ola", IsAdminUser = false,
                HashedPassword = "$2a$11$l9QLO86QyxuaJrhM2MKam.y192KcZ1.KiSIgxZA18A/GPWqKi9qQW",
                Created = new DateTime(2023, 11, 14, 9, 30, 0),
                Updated = new DateTime(2023, 11, 14, 9, 30, 0),
            },
            new()
            {
                FirstName = "Kari", LastName = "Normann", Email = "kari@gmail.com",
                UserName = "kari", IsAdminUser = false, HashedPassword = "jølsdkfjasødlfkhasødfhasødfh",
                Id = Guid.NewGuid(),
                Created = new DateTime(2023, 11, 14, 9, 30, 0),
                Updated = new DateTime(2020, 11, 14, 9, 30, 0),
            }
        ];
        
        // Authorization: Basic b2xhOm9sYQ== (registrated user) 
        _client.DefaultRequestHeaders.Add("Authorization", "Basic b2xhOm9sYQ==");
        
        // setup repository returns!
        // var usr = (await _userRepository.FindAsync(expr)).FirstOrDefault(); (UserService:AuthenticateUserAsync)
        _factory.UserRepositoryMock.Setup(x =>
            x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>(){users[0]});
        
        // var users = await _userRepository.GetPagedAsync(pageNumber, pageSize); (UserService:GetPagedAsync())
        _factory.UserRepositoryMock.Setup(x =>
                x.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(users);
        
        
        // Act
        var response = await _client.GetAsync("/api/v1/users");
        
        
        // Assert
        var userDtos = JsonConvert
            .DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Collection(userDtos,
            u =>
            {
                Assert.Equal(u.Email, users[0].Email);
                Assert.Equal(u.UserName, users[0].UserName);
                Assert.Equal(u.Id, users[0].Id);
                Assert.Equal(u.Created, users[0].Created);
                Assert.Equal(u.Updated, users[0].Updated);
                Assert.Equal(u.FirstName, users[0].FirstName);
                Assert.Equal(u.LastName, users[0].LastName);
            },
            u =>
            {
                Assert.Equal(u.Email, users[1].Email);
                Assert.Equal(u.UserName, users[1].UserName);
                Assert.Equal(u.Id, users[1].Id);
                Assert.Equal(u.Created, users[1].Created);
                Assert.Equal(u.Updated, users[1].Updated);
                Assert.Equal(u.FirstName, users[1].FirstName);
                Assert.Equal(u.LastName, users[1].LastName);   
            });
    }
    
    [Theory]
    [MemberData(nameof(TestUsers.GetTestUsers), MemberType = typeof(TestUsers))]
    public async Task GetUsers_WhenNoSearchParams_ThenReturnsPagedUsers_Theory(TestUser testUser)
    {
        // Arrange
        User user = testUser.User!;
        
        // setup repository returns !!
        _factory.UserRepositoryMock.Setup(x => 
                x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>(){user});
        _factory.UserRepositoryMock.Setup(x =>
                x.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<User>(){user});
        
        // Act
        
        // user: Ola -> Authorization: Basic b2xhOm9sYQ==
        _client.DefaultRequestHeaders.Add("Authorization", $"Basic {testUser.Base64EncodedUsernamePassword}");
        var response = await _client.GetAsync("/api/v1/users");
        
        // Assert
        var userDtos = JsonConvert
            .DeserializeObject<IEnumerable<UserDTO>>(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userDtos);
        Assert.Collection(userDtos,
            u =>
            {
                Assert.Equal(u.Email, user.Email);
                Assert.Equal(u.FirstName, user.FirstName);
                Assert.Equal(u.LastName, user.LastName);
                Assert.Equal(u.Email, user.Email);
                Assert.Equal(u.UserName, user.UserName);
                Assert.Equal(u.Id, user.Id);
                Assert.Equal(u.Created, user.Created);
            });
    }
}