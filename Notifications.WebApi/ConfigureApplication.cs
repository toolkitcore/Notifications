namespace Notifications.WebApi;

public static class ConfigureApplication
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMonitoring(this WebApplication app)
    {


        return app;
    }
}