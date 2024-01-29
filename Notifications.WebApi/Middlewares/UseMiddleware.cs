using Microsoft.AspNetCore.Builder;

namespace Notifications.WebApi.Middlewares;

public static class UseMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorExceptionMiddleware>();
    }
}