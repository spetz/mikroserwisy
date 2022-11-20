using System.Diagnostics.CodeAnalysis;
using Micro.DAL.Postgres;
using Micro.Testing;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Infrastructure.DAL;

namespace VideoHub.Services.Videos.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public VideosDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        Context = new VideosDbContext(new DbContextOptionsBuilder<VideosDbContext>().UseNpgsql(options.ConnectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}