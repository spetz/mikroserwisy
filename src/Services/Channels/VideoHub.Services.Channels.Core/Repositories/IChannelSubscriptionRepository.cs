using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.Repositories;

public interface IChannelSubscriptionRepository
{
    Task<ChannelSubscription?> GetAsync(long channelId, long userId);
    Task AddAsync(ChannelSubscription subscription);
    Task DeleteAsync(ChannelSubscription subscription);
}