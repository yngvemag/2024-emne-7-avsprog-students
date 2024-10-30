using System.Data;
using FluentValidation;
using StudentBloggAPI.Features.Users;

namespace StudentBloggAPI.Validators;

public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDTO>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .Length(3,20).WithMessage("Username must be between 3 and 20 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required")
            .Length(2, 30).WithMessage("Firstname must be between 2 and 30 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname is required")
            .Length(2, 30).WithMessage("Lastname must be between 2 and 30 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(8, 16).WithMessage("Password must be between 8 and 16 characters")
            .Matches("[0-9]+").WithMessage("Invalid password, need at least 1 number")
            .Matches("[A-Z]+").WithMessage("Invalid password, need at least 1 capital letter")
            .Matches("[a-z]+").WithMessage("Invalid password, need at least 1 lowercase letter")
            .Matches("[!?*#_]+").WithMessage("Invalid password, need at least one of the characters '(! ? * # _)'")
            .Must(password => !password.Any(c => "æøåÆØÅ"
                .Contains(c))).WithMessage("Password contains invalid characters.'(æ ø å Æ Ø Å)'");
            
        
        //.Matches("[^æÆøØåÅ]+").WithMessage("Invalid password, cannot contain any of the following characters '(æ ø å)'");
        //.Matches("^((?![æøå]+).)$").WithMessage("Invalid password, cannot contain any of the following characters '(æ ø å)'");

    }
}