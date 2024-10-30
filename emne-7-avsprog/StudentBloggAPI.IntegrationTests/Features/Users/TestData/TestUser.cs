using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.IntegrationTests.Features.Users.TestData;

public class TestUser
{
    public User? User { get; set; }
    public string Base64EncodedUsernamePassword { get; init; } = string.Empty;
}