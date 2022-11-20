using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.DAL;

namespace VideoHub.Services.Channels.Tests.Integration;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public ChannelsDbContext Context { get; }

    public TestDatabase()
    {
        var connectionString = $"Host=localhost;Database=videohub-channels-tests-{Guid.NewGuid():N};Username=postgres;Password=";
        Context = new ChannelsDbContext(new DbContextOptionsBuilder<ChannelsDbContext>().UseNpgsql(connectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public Task InitAsync() => Context.Database.MigrateAsync();

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}