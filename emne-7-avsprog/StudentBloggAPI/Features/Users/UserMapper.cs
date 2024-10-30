using StudentBloggAPI.Features.Common.Interfaces;

namespace StudentBloggAPI.Features.Users;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User model)
    {
        return new UserDTO()
        {
            Id = model.Id,
            Email = model.Email,
            Created = model.Created,
            Updated = model.Updated,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName
        };
    }

    public User MapToModel(UserDTO dto)
    {
        return new User()
        {
            Created = dto.Created,
            FirstName = dto.FirstName,
            Email = dto.Email,
            UserName = dto.UserName,
            Updated = dto.Updated,
            Id = dto.Id,
            LastName = dto.LastName
            // HashedPassword => registrerings process -> da blir denne satt
            // IsAdminUser => ved innlogging -> httpcontext som vi senere kan bruke !
        };
    }
}