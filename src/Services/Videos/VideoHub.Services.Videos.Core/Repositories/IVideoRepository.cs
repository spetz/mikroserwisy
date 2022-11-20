using VideoHub.Services.Videos.Core.Entities;

namespace VideoHub.Services.Videos.Core.Repositories;

public interface IVideoRepository
{
    Task<Video?> GetAsync(long id);
    Task AddAsync(Video video);
    Task UpdateAsync(Video video);
    Task DeleteAsync(Video video);
}