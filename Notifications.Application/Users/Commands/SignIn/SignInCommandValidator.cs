using FluentValidation;

namespace Notifications.Application.Users.Commands.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Code)
            .MaximumLength(50)
            .NotEmpty();

    }
}
