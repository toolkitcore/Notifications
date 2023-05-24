using FluentValidation;

namespace Notifications.Application.Apps.Commands.UpdateApp;

public class UpdateAppCommandValidator : AbstractValidator<UpdateAppCommand>
{
    public UpdateAppCommandValidator()
    {
        RuleFor(a => a.Code).MaximumLength(50).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(250).NotEmpty();
        RuleFor(a => a.LogoUrl).MaximumLength(250);
        RuleFor(a => a.SortName).MaximumLength(50);
    }
}