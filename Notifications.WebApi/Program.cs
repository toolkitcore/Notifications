using Notifications.Infrastructure;
using Notifications.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddControllers();
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
    }
    
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    
}

app.Run();