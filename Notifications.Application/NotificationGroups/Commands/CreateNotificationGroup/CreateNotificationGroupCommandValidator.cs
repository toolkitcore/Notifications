using FluentValidation;

namespace Notifications.Application.NotificationGroups.Commands.CreateNotificationGroup;

public class CreateNotificationGroupCommandValidator : AbstractValidator<CreateNotificationGroupCommand>
{
    public CreateNotificationGroupCommandValidator()
    {
        RuleFor(a => a.Code).MaximumLength(50).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(250).NotEmpty();
    }
}