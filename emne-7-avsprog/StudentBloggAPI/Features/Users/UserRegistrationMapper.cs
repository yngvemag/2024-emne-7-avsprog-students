using StudentBloggAPI.Features.Common.Interfaces;

namespace StudentBloggAPI.Features.Users;

public class UserRegistrationMapper : IMapper<User, UserRegistrationDTO>
{
    public UserRegistrationDTO MapToDTO(User model)
    {
        // Denne får vi nok aldri bruk for !! Hvorfor? Skal ikke tilbake til client!
        return new UserRegistrationDTO()
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }

    public User MapToModel(UserRegistrationDTO dto)
    {
        return new User()
        {
            Email = dto.Email ?? string.Empty,
            FirstName = dto.FirstName!,
            LastName = dto.LastName!,
            UserName = dto.UserName!
        };
    }
}