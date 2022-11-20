using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Repositories;

namespace VideoHub.Services.Videos.Infrastructure.DAL.Repositories;

internal sealed class VideoRepository : IVideoRepository
{
    private readonly VideosDbContext _dbContext;

    public VideoRepository(VideosDbContext dbContext)
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

    public async Task UpdateAsync(Video video)
    {
        _dbContext.Videos.Update(video);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Video video)
    {
        _dbContext.Videos.Remove(video);
        await _dbContext.SaveChangesAsync();
    }
}