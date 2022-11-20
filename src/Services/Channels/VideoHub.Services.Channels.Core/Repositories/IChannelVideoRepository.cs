using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.Repositories;

public interface IChannelVideoRepository
{
    Task<ChannelVideo?> GetAsync(long channelId, long videoId);
    Task AddAsync(ChannelVideo video);
    Task DeleteAsync(ChannelVideo video);
}