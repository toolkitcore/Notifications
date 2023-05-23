using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly ILogger<ApplicationDbContextSeed> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextSeed(ILogger<ApplicationDbContextSeed> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while seeding the database;");
            throw;
        }
    }


    public async Task TrySeedAsync()
    {
        if (!_context.Apps.Any())
        {
            await _context.Apps.AddRangeAsync(
                new List<App>()
                {
                    new App() {Code = "1", Name = "App1"},
                    new App() {Code = "2", Name = "App2"},
                    new App() {Code = "3", Name = "App3"},
                    new App() {Code = "4", Name = "App4"},
                    new App() {Code = "5", Name = "App5"},
                }
            );
        }
    }

    public async Task<IEnumerable<App>> GetAllAsync()
    {
        return await _context.Apps.ToListAsync();
    }
}