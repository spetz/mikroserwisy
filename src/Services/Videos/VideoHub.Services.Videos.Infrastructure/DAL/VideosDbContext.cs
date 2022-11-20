using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Core.Entities;

namespace VideoHub.Services.Videos.Infrastructure.DAL;

internal sealed class VideosDbContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
        
    public VideosDbContext(DbContextOptions<VideosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}