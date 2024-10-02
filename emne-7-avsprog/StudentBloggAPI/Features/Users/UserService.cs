using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using StudentBloggAPI.Features.Common.Interfaces;
using StudentBloggAPI.Features.Users.Interfaces;

namespace StudentBloggAPI.Features.Users;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _registrationMapper;
    private readonly IUserRepository _userRepository;


    public UserService(ILogger<UserService> logger, 
        IMapper<User, UserDTO> userMapper, 
        IMapper<User, UserRegistrationDTO> registrationMapper, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _userMapper = userMapper;
        _registrationMapper = registrationMapper;
        _userRepository = userRepository;
    }
    public async Task<UserDTO?> AddAsync(UserDTO dto)
    {
        var model = _userMapper.MapToModel(dto);
        var modelResponse = await _userRepository.AddAsync(model);
        return modelResponse is null
            ? null
            : _userMapper.MapToDTO(modelResponse);
    }

    public Task<UserDTO?> UpdateAsync(UserDTO entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        var model = await _userRepository.GetByIdAsync(id);
        return model is null
            ? null
            : _userMapper.MapToDTO(model);
    }

    public async Task<IEnumerable<UserDTO>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var users = await _userRepository.GetPagedAsync(pageNumber, pageSize);

        return users
            .Select(usr => _userMapper.MapToDTO(usr))
            .ToList();
        
        // List<UserDTO> dtos = [];
        // foreach (var usr in users)
        // {
        //     dtos.Add(_userMapper.MapToDTO(usr));
        // }
        // // mappe data User -> UserDTO
        // return dtos;
        //
        // // return liste av UserDTO`s 
    }

    public async Task<UserDTO?> RegisterAsync(UserRegistrationDTO regDto)
    {
        var user = _registrationMapper.MapToModel(regDto);
        user.Id = Guid.NewGuid();
        user.Created = DateTime.UtcNow;
        user.Updated = DateTime.UtcNow;
        user.IsAdminUser = false;
        
        // legger til hashed password !
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(regDto.Password);
        
        // Legge til user i database
        var addedUser = await _userRepository.AddAsync(user);

        if (addedUser is null) 
            return null;
        
        // return UserDTO
        return _userMapper.MapToDTO(addedUser);
    }

    public async Task<Guid> AuthenticateUserAsync(string user, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDTO>> FindAsync(UserSearchParams searchParams)
    {
        // bygge opp predicate dynamsik
        Expression<Func<User, bool>> predicate = u =>
            (string.IsNullOrEmpty(searchParams.UserName) || u.UserName.Contains(searchParams.UserName)) &&
            (string.IsNullOrEmpty(searchParams.FirstName) || u.FirstName.Contains(searchParams.FirstName)) &&
            (string.IsNullOrEmpty(searchParams.LastName) || u.LastName.Contains(searchParams.LastName)) &&
            (string.IsNullOrEmpty(searchParams.Email) || u.Email.Contains(searchParams.Email));

        var users = await _userRepository.FindAsync(predicate);
        
        return users.Select(u => _userMapper.MapToDTO(u));
    }
}