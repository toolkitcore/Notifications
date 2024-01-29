using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NotificationGroupPublisher.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ISender"></typeparam>
    /// <returns></returns>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
