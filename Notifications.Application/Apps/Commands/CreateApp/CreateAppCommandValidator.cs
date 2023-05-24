using FluentValidation;

namespace Notifications.Application.Apps.Commands.CreateApp;

public class CreateAppCommandValidator : AbstractValidator<CreateAppCommand>
{
    public CreateAppCommandValidator()
    {
        RuleFor(a => a.Code).MaximumLength(50).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(250).NotEmpty();
        RuleFor(a => a.LogoUrl).MaximumLength(250);
        RuleFor(a => a.SortName).MaximumLength(50);
    }
}