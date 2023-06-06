using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Notifications.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder
            .UseNpgsql(@"Server=db;Port=5432;User Id=postgres;Password=postgres;Database=Notifications;", 
                opts =>
                {
                    opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds); 
                }
                );
        
        var operationalStoreOptions = new OperationalStoreOptions(); // Tạo một instance mới của OperationalStoreOptions

        var context = new ApplicationDbContext(optionsBuilder.Options, Options.Create(operationalStoreOptions));
        
        return context;
    }
}