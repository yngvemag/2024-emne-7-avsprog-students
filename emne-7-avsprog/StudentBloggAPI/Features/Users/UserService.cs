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
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserService(ILogger<UserService> logger, 
        IMapper<User, UserDTO> userMapper, 
        IMapper<User, UserRegistrationDTO> registrationMapper, 
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userMapper = userMapper;
        _registrationMapper = registrationMapper;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var loggedInUserId = _httpContextAccessor.HttpContext?.Items["UserId"] as string;
        
        // vi må hente innlogget bruker fra databasen.
        var loggedInUser = (await _userRepository.FindAsync(u => u.Id.ToString() == loggedInUserId))
            .FirstOrDefault();

        if (loggedInUser == null)
        {
            _logger.LogWarning("Did not find logged in user with id={UserID}", loggedInUserId);
            return false;
        }
        
        // har vi lov til å slette !!
        if (id.ToString().Equals(loggedInUser.Id.ToString()) || loggedInUser.IsAdminUser)
        {
            _logger.LogDebug("Deleting user with id: {UserId}", loggedInUserId);
            var deletedUser =  await _userRepository.DeleteByIdAsync(id);

            if (deletedUser == null)
            {
                _logger.LogWarning("Did not delete user with id: {UserId}", id);
                return false;
            }
            
            return true;
        }

        return false;
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

    public async Task<Guid> AuthenticateUserAsync(string userName, string password)
    {
        Expression<Func<User, bool>> expr = user => user.UserName == userName;
        var usr = (await _userRepository.FindAsync(expr)).FirstOrDefault();
        if (usr is null) return Guid.Empty;

        // sjekker om passord stemmer !!
        if (BCrypt.Net.BCrypt.Verify(password, usr.HashedPassword))
            return usr.Id;
        
        return Guid.Empty;
        ;
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