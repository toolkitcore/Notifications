using Notifications.Application.Common.Mappings;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Models.Apps;

public class AppDto : IMapFrom<App>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string SortName { get; set; }
}