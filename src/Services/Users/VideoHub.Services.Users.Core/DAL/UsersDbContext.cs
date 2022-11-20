using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.Entities;

namespace VideoHub.Services.Users.Core.DAL;

internal sealed class UsersDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;
        
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}