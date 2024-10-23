using StudentBloggAPI.Features.Common.Interfaces;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.UnitTests.Features.Users;

public class UserMapperTests
{
    private readonly IMapper<User, UserDTO> _userMapper = new UserMapper();

    [Fact]
    public void MapToDTO_When_UserModelIsValid_Should_Return_UserDTO()
    {
        // Arrange -> klargjøre data som vi trenger til test
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = "email@email.com",
            FirstName = "Ola",
            LastName = "Normann",
            UserName = "ola_normann",
            IsAdminUser = false,
            Updated = new DateTime(2024, 10, 23, 9, 45, 00 ),
            Created = new DateTime(2024, 10, 23, 9, 45, 00 ),
            HashedPassword = "øsladkfjsaøldfkjasødlfkjasdølfkjasødl" 
        };

        // Act
        UserDTO userDTO = _userMapper.MapToDTO(user);

        // Assert
        Assert.NotNull(userDTO);
        Assert.Equal(user.Id, userDTO.Id);
        Assert.Equal(user.Email, userDTO.Email);
        Assert.Equal(user.FirstName, userDTO.FirstName);
        Assert.Equal(user.LastName, userDTO.LastName);
        Assert.Equal(user.UserName, userDTO.UserName);
        Assert.Equal(user.Updated, userDTO.Updated);
        Assert.Equal(user.Created, userDTO.Created);
    }

    [Fact]
    public void MapToModel_When_UserDTOIsValid_Should_Return_User()
    {
        // Arrange
        UserDTO dto = new()
        {
            Id = Guid.NewGuid(),
            Email = "email@email.com",
            FirstName = "Ola",
            LastName = "Normann",
            UserName = "ola_normann",
            Created = new DateTime(2024, 10, 23, 9, 45, 00),
            Updated = new DateTime(2024, 10, 23, 9, 45, 00),
        };
        
        // Act
        User user = _userMapper.MapToModel(dto);
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal(dto.Id, user.Id);
        Assert.Equal(dto.Email, user.Email);
        Assert.Equal(dto.FirstName, user.FirstName);
        Assert.Equal(dto.LastName, user.LastName);
        Assert.Equal(dto.UserName, user.UserName);
        Assert.Equal(dto.Updated, user.Updated);
        Assert.Equal(dto.Created, user.Created);
    }
        
}