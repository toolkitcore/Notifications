using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.WebApi.Filters;

namespace Notifications.WebApi.Controllers;

[ApiController]
[ApiExceptionFilter]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
