using Notifications.Application;
using Notifications.Infrastructure;
using Notifications.Infrastructure.Persistence;
using Notifications.WebApi;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddWebApiServices(); ;
}

var app = builder.Build();

{
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var applicationDbContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeed>();
        await applicationDbContextSeed.InitialiseAsync();
        await applicationDbContextSeed.SeedAsync();
    }
    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();