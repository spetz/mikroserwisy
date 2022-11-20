using VideoHub.Services.Channels.Core.Clients.DTO;

namespace VideoHub.Services.Channels.Core.Clients;

public interface IVideosApiClient
{
    Task<VideoDto?> GetVideoAsync(long videoId);
}