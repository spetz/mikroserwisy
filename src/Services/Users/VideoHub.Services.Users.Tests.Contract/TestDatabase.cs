using System.Diagnostics.CodeAnalysis;
using Micro.DAL.Postgres;
using Micro.Testing;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.DAL;

namespace VideoHub.Services.Users.Tests.Contract;

[ExcludeFromCodeCoverage]
internal sealed class TestDatabase : IDisposable
{
    public UsersDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        Context = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().UseNpgsql(options.ConnectionString).Options);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}