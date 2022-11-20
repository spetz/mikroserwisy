using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.DAL.Repositories;

internal sealed class ChannelSubscriptionRepository : IChannelSubscriptionRepository
{
    private readonly ChannelsDbContext _dbContext;

    public ChannelSubscriptionRepository(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ChannelSubscription?> GetAsync(long channelId, long userId)
        => _dbContext.ChannelSubscriptions.SingleOrDefaultAsync(x => x.ChannelId == channelId && x.UserId == userId);

    public async Task AddAsync(ChannelSubscription subscription)
    {
        await _dbContext.ChannelSubscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChannelSubscription subscription)
    {
        _dbContext.ChannelSubscriptions.Remove(subscription);
        await _dbContext.SaveChangesAsync();
    }
}