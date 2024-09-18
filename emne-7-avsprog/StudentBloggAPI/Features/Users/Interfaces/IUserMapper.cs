namespace StudentBloggAPI.Features.Users.Interfaces;

public interface IUserMapper
{
    UserDTO MapToDTO(User model);
    User MapToModel(UserDTO dto);
}