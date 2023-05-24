using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Apps.Commands.CreateApp;
using Notifications.Application.Apps.Commands.DeleteApp;
using Notifications.Application.Apps.Commands.UpdateApp;
using Notifications.Application.Apps.Queries.GetApp;
using Notifications.Application.Common.Models.Apps;
using Notifications.Domain.Entities;

namespace Notifications.WebApi.Controllers;

public class AppController : ApiControllerBase
{
    [HttpGet]
    [Route("api/apps/{id:guid}")]
    public async Task<ActionResult<AppDto>> GetAsync([FromRoute(Name = "id")]Guid appId, CancellationToken cancellationToken = default)
    {
        return await Mediator.Send(new GetAppQuery(appId), cancellationToken);
    }
    
    [HttpPost]
    [Route("api/apps")]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody]CreateAppCommand query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPut]
    [Route("api/apps/{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateAsync([FromRoute(Name = "id")]Guid appId, [FromBody]UpdateAppCommand query)
    {
        if (appId != query.Id)
        {
            return BadRequest();
        }
        
        await Mediator.Send(query);

        return NoContent();
    }
    
    
    [HttpDelete]
    [Route("api/apps/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAsync([FromRoute(Name = "id")]Guid appId)
    {
        await Mediator.Send(new DeleteAppCommand(appId));

        return NoContent();
    }
    
}