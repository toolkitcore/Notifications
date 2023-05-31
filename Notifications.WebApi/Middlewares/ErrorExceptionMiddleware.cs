using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Notifications.Application.Common.Models.Abstractions;
using ApplicationException = Notifications.Application.Common.Exceptions.ApplicationException;

namespace Notifications.WebApi.Middlewares;

public class ErrorExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorExceptionMiddleware> _logger;

    public ErrorExceptionMiddleware(RequestDelegate next, ILogger<ErrorExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.StackTrace);
            await HandleExceptionAsync(httpContext, ex, _logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorExceptionMiddleware> logger)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        if (exception is UnauthorizedAccessException)
        {
            logger?.LogWarning("Unauthorized request");
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        var (statusCode, modelResult) = ErrorExceptionMiddlewareHandler.Handle(exception);

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(
            JsonConvert.SerializeObject(modelResult, jsonSettings));
    }
}

public static class ErrorExceptionMiddlewareHandler
{
    public static (int statusCode, ResultModel resultModel) Handle(Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                var validationErrorModel = ResultModel
                    .Failure(validationException.Errors.GroupBy(x => x.PropertyName)
                        .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToList()));
                return ((int)HttpStatusCode.BadRequest, validationErrorModel);
            
            case ApplicationException applicationException:
                return ((int)HttpStatusCode.BadRequest, ResultModel.Failure(applicationException.ErrorKey));

            default:
            {
                return ((int)HttpStatusCode.InternalServerError, ResultModel.Failure(exception.Message));
            }
        }
    }
}