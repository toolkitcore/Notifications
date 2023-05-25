using FluentValidation;
using Notifications.Application.NotificationGroups.Commands.CreateNotificationGroup;

namespace Notifications.Application.NotificationGroups.Commands.UpdateNotificationGroup;

public class UpdateNotificationGroupCommandValidator : AbstractValidator<CreateNotificationGroupCommand>
{
    public UpdateNotificationGroupCommandValidator()
    {
        RuleFor(a => a.Code).MaximumLength(50).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(250).NotEmpty();
    }
}