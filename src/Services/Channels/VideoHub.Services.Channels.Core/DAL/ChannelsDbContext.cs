using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.DAL;

internal sealed class ChannelsDbContext : DbContext
{
    public DbSet<Channel> Channels { get; set; } = null!;
    public DbSet<ChannelSubscription> ChannelSubscriptions { get; set; } = null!;
    public DbSet<ChannelVideo> ChannelVideos { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
        
    public ChannelsDbContext(DbContextOptions<ChannelsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}