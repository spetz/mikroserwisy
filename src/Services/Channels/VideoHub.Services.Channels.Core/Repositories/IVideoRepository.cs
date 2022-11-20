using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.Repositories;

public interface IVideoRepository
{
    Task<Video?> GetAsync(long id);
    Task AddAsync(Video video);
    Task DeleteAsync(Video video);
}