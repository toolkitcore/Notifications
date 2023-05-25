using FluentValidation;

namespace Notifications.Application.NotificationGroups.Queries.GetNotificationGroupsWithPaginationQuery;

public class GetNotificationGroupsWithPaginationQueryValidator : AbstractValidator<GetNotificationGroupsWithPaginationQuery>
{
    public GetNotificationGroupsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("PageIndex at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}