using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.IntegrationTests.Features.Users.TestData;

public class TestUsers
{
    public static IEnumerable<object[]> GetTestUsers() => new List<object[]>()
    {
        new object[]
        { 
            new TestUser 
            { 
                Base64EncodedUsernamePassword = "b2xhOm9sYQ==",
                User = new User 
                {
                    Created = new DateTime(2023, 11, 14, 9, 30, 0),
                    Email = "ola@gmail.com",
                    FirstName = "Ola",
                    UserName = "ola",
                    LastName = "Normann",
                    Id = Guid.Parse("3e2385f8-84b6-4e9f-962f-414391a8e7de"),
                    IsAdminUser = false,
                    HashedPassword = "$2a$11$l9QLO86QyxuaJrhM2MKam.y192KcZ1.KiSIgxZA18A/GPWqKi9qQW",
                    Updated = new DateTime(2023, 11, 14, 9, 30, 0)
                }
            }
        },
        new object[]
        { 
            new TestUser
            { 
                Base64EncodedUsernamePassword = "eW1hOmhlbW1lbGln",
                User = new()
                {
                    Created = new DateTime(2023, 11, 14, 9, 30, 0),
                    Email = "yngve@mail.com",
                    FirstName = "Yngve",
                    UserName = "yma",
                    LastName = "Magnussen",
                    Id = Guid.Parse("3e2385f8-84b6-4e9f-962f-414391a8e7dd"),
                    IsAdminUser = true,
                    HashedPassword = "$2a$11$FYEtTPFPJAPpwmnaNNMg5uA5u33w0c4hVmUcxKZwZI0vn2vVVk0Ym",
                    Updated = new DateTime(2023, 11, 14, 9, 30, 0)
                }
            }
        }
    };

}