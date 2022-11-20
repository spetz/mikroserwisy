using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.DAL.Repositories;

internal sealed class ChannelVideoRepository : IChannelVideoRepository
{
    private readonly ChannelsDbContext _dbContext;

    public ChannelVideoRepository(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ChannelVideo?> GetAsync(long channelId, long videoId)
        => _dbContext.ChannelVideos.SingleOrDefaultAsync(x => x.ChannelId == channelId && x.VideoId == videoId);

    public async Task AddAsync(ChannelVideo video)
    {
        await _dbContext.ChannelVideos.AddAsync(video);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChannelVideo video)
    {
        _dbContext.ChannelVideos.Remove(video);
        await _dbContext.SaveChangesAsync();
    }
}