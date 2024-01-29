using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Notifications.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{

     private readonly Stopwatch _timer;
     private readonly ILogger<TRequest> _logger;

     /// <summary>
     /// 
     /// </summary>
     /// <param name="logger"></param>
     public PerformanceBehaviour(ILogger<TRequest> logger)
     {
          _timer = new Stopwatch();
          _logger = logger;
     }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="request"></param>
     /// <param name="next"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
     public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
     {
          _timer.Start();
          var response = await next();
          _timer.Stop();

          var elapsedMilliseconds = _timer.ElapsedMilliseconds;

          if (elapsedMilliseconds <= 500) return response;

          var requestName = typeof(TRequest).Name;
          
          _logger.LogWarning("Application Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
               requestName, elapsedMilliseconds, request);

          return response;
     }
}
