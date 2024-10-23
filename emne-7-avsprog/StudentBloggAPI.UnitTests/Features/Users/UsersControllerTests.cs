using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StudentBloggAPI.Features.Users;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.UnitTests.Features.Users;

public class UsersControllerTests
{
    private readonly UsersController _usersController;
    private readonly Mock<ILogger<UsersController>> _mockLogger  = new Mock<ILogger<UsersController>>();
    private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();

    public UsersControllerTests()
    {
        _usersController = new UsersController(
            _mockLogger.Object, 
            _mockUserService.Object);   
    }

    [Fact]
    public async Task GetUsersAsync_WhenDefaultPageSizeAndOneUserExists_ShouldReturnOneUser()
    {
        // Arrange
        string userName = "ola_normann";
        List<UserDTO> dtos = [
            new UserDTO()
            {
                Id = Guid.NewGuid(), UserName = userName, FirstName = "Ola",
                LastName = "Normann", Email = "ola_normann@gmail.com", Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            }
        ];
        
        // _userService.GetPagedAsync(pageNr,pageSize
        _mockUserService.Setup( x => 
            x.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(dtos);
        

        // Act
        var result = await _usersController.GetUsersAsync(new UserSearchParams());

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<UserDTO>>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
        var userDtos = Assert.IsType<List<UserDTO>>(returnValue.Value);
        
        var dto = userDtos.FirstOrDefault();
        Assert.NotNull(dto);
        Assert.Equal(dto.UserName, userName);
    }
}