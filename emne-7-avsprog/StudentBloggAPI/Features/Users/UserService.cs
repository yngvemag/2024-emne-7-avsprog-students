using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using StudentBloggAPI.Features.Common.Interfaces;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Features.Users;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper<User, UserDTO> _userMapper;


    public UserService(ILogger<UserService> logger, IMapper<User, UserDTO> userMapper)
    {
        _logger = logger;
        _userMapper = userMapper;
    }
    public Task<UserDTO?> AddAsync(UserDTO entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> UpdateAsync(UserDTO entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDTO>> GetPagedAsync(int pageNumber, int pageSize)
    {
        await Task.Delay(20);
        // vi henter model fra databasen!!
        var model = new User()
        {
            Id = Guid.NewGuid(),
            UserName = "Ola",
            FirstName = "Ola",
            LastName = "Normann",
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Email = "ola@mail.com",
            HashedPassword = "dksjføasjføsaljføsadjaødkjsaødf",
            IsAdminUser = true
        };
    
        // MAPPING -> FRA User -> UserDTO
        var dto = _userMapper.MapToDTO(model);
        
        // legg i liste og return to controller
        return new List<UserDTO>() { dto };
    }

    public Task<IEnumerable<UserDTO>> FindAsync(Expression<Func<UserDTO, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}