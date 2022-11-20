using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.DAL.Repositories;

internal sealed class VideoRepository : IVideoRepository
{
    private readonly ChannelsDbContext _dbContext;

    public VideoRepository(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Video?> GetAsync(long id)
        => _dbContext.Videos.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Video video)
    {
        await _dbContext.Videos.AddAsync(video);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Video video)
    {
        _dbContext.Videos.Remove(video);
        await _dbContext.SaveChangesAsync();
    }
}