using FluentValidation;

namespace Notifications.Application.Users.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    /// <summary>
    /// 
    /// </summary>
    public SignUpCommandValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(50)
            .NotEmpty()
            .WithMessage("Do Chi Hung");

        RuleFor(x => x.Code)
            .MaximumLength(50)
            .NotEmpty();
        
        RuleFor(x => x.CountryCode)
            .MaximumLength(50)
            .NotEmpty();
    }
}
