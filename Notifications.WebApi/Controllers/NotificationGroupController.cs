using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.NotificationGroups.Commands.Create;
using Notifications.Application.NotificationGroups.Commands.Delete;
using Notifications.Application.NotificationGroups.Commands.Update;
using Notifications.Application.NotificationGroups.Models;
using Notifications.Application.NotificationGroups.Queries.Get;
using Notifications.Application.NotificationGroups.Queries.GetsWithPaginationQuery;

namespace Notifications.WebApi.Controllers;

[Authorize(Roles = "Admin")]
public class NotificationGroupController : ApiControllerBase
{
    [HttpGet]
    [Route("api/notification-groups")]
    public async Task<ActionResult<PaginatedList<NotificationGroupDto>>> GetAllAsync([FromQuery]GetNotificationGroupsWithFilterRequestModel query)
    {
        return await Mediator.Send(query).ConfigureAwait(false);
    }
    
    [HttpGet]
    [Route("api/notification-groups/{id:guid}")]
    public async Task<ActionResult<NotificationGroupDto>> GetAsync([FromRoute(Name = "id")]Guid notificationGroupId)
    {
        return await Mediator.Send(new GetNotificationGroupQuery(notificationGroupId));
    }

    [HttpPost]
    [Route("api/notification-groups")]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody]CreateNotificationGroupCommand command)
    {
        return await Mediator.Send(command).ConfigureAwait(false);
    }
    
    [HttpPut]
    [Route("api/notification-groups/{id:guid}")]
    public async Task<ActionResult> UpdateAsync([FromRoute(Name = "id")]Guid notificationGroupId, [FromBody]UpdateNotificationGroupCommand command)
    {
        await Mediator.Send(command).ConfigureAwait(false);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("api/notification-groups/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAsync([FromRoute(Name = "id")] Guid notificationGroupId)
    {
        await Mediator.Send(new DeleteNotificationGroupCommand(notificationGroupId));
        return NoContent();
    }

}