using System.ComponentModel.DataAnnotations;

namespace StudentBloggAPI.Features.Users;

public class UserRegistrationDTO
{
    //[Required(ErrorMessage = "Username is required")]
    //[StringLength(20, ErrorMessage = "Username must be between 3 and 20 characters", MinimumLength = 3)]
    public string? UserName { get; set; }
    
    //[Required(ErrorMessage = "FirstName is required")]
    //[MinLength(2, ErrorMessage = "First name must be at least 2 characters"),MaxLength(30, ErrorMessage = "First name must be between 2 and 30 characters")] 
    public string? FirstName { get; set; }
    
    
    //[Required(ErrorMessage = "LastName is required")]
    //[MinLength(2, ErrorMessage = "LastName must be at least 2 characters"),MaxLength(30, ErrorMessage = "Last name must be between 2 and 30 characters")]
    public string? LastName { get; set; }
    
    //[Required(ErrorMessage = "Email is required")]
    //[EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    
    //[Required(ErrorMessage = "Password is required")]
    //[RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!?*#_]).{8,16}$",
      //  ErrorMessage = "Passordet må være mellom 8 og 16 tegn og inneholde minst én stor bokstav, én liten bokstav, ett tall, og ett av spesialtegnene (! ? * # _)")]
    public string? Password { get; set; }
}