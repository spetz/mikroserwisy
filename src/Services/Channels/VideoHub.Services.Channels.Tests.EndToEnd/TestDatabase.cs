using System.Diagnostics.CodeAnalysis;
using Micro.DAL.Postgres;
using Micro.Testing;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.DAL;

namespace VideoHub.Services.Channels.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public ChannelsDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        Context = new ChannelsDbContext(new DbContextOptionsBuilder<ChannelsDbContext>().UseNpgsql(options.ConnectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}