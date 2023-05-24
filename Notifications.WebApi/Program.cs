using Notifications.Application;
using Notifications.Infrastructure;
using Notifications.Infrastructure.Persistence;
using Notifications.WebApi;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddWebApiServices();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

{
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var applicationDbContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeed>();
        await applicationDbContextSeed.InitialiseAsync();
        await applicationDbContextSeed.SeedAsync();

        var app_ = await applicationDbContextSeed.GetAllAsync();
        var a = "";
    }
    
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    
}

app.Run();