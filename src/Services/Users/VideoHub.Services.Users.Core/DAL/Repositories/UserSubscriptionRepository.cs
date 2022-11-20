using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.Entities;
using VideoHub.Services.Users.Core.Repositories;

namespace VideoHub.Services.Users.Core.DAL.Repositories;

internal class UserSubscriptionRepository : IUserSubscriptionRepository
{
    private readonly UsersDbContext _context;

    public UserSubscriptionRepository(UsersDbContext context)
    {
        _context = context;
    }

    public Task<UserSubscription?> GetAsync(long userId)
        => _context.UserSubscriptions.SingleOrDefaultAsync(x => x.UserId == userId);

    public async Task AddAsync(UserSubscription subscription)
    {
        await _context.UserSubscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserSubscription subscription)
    {
        _context.UserSubscriptions.Update(subscription);
        await _context.SaveChangesAsync();
    }
}