using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.Common.Models.Users;
using Notifications.Application.Users.Commands.SignIn;
using Notifications.Application.Users.Commands.SignUp;
using Notifications.Application.Users.Queries.GetUsersWithPagination;

namespace Notifications.WebApi.Controllers;

public class UserController : ApiControllerBase
{
    [HttpGet]
    [Route("api/users")]
    public async Task<ActionResult<PaginatedList<UserDto>>> GetTodoItemsWithPaginationAsync([FromQuery] GetUsersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    [Route("api/users/sign-up")]
    public async Task<ActionResult<AuthenticationResult>> SignUpAsync([FromBody]SignUpCommand query)
    {
        return await Mediator.Send(query);
    }

}