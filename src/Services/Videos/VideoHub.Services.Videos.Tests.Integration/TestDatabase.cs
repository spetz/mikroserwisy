using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Infrastructure.DAL;

namespace VideoHub.Services.Videos.Tests.Integration;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public VideosDbContext Context { get; }

    public TestDatabase()
    {
        var connectionString = $"Host=localhost;Database=videohub-videos-tests-{Guid.NewGuid():N};Username=postgres;Password=";
        Context = new VideosDbContext(new DbContextOptionsBuilder<VideosDbContext>().UseNpgsql(connectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public Task InitAsync() => Context.Database.MigrateAsync();

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}