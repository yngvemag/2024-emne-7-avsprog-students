using JwtAuthorization.Models.Interfaces;

namespace JwtAuthorization.Models;

public interface IIdentityUser
{
    Guid? Id { get; set; }
    string? UserName { get; set; }
    ICollection<IRole> Roles { get; set; }
}