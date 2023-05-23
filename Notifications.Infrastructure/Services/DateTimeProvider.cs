using Notifications.Application.Common.Interfaces;

namespace Notifications.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get; }
}