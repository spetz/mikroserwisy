using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.Repositories;

public interface IChannelRepository
{
    Task<Channel?> GetAsync(long id);
    Task<Channel?> GetAsync(string name);
    Task AddAsync(Channel channel);
    Task DeleteAsync(Channel channel);
}