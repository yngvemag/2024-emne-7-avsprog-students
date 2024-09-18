namespace StudentBloggAPI.Features.Users.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
}