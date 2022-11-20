using Micro.Handlers;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Events.External.Handlers;

internal sealed class VideoDeletedHandler : IEventHandler<VideoDeleted>
{
    private readonly IVideoRepository _videoRepository;

    public VideoDeletedHandler(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task HandleAsync(VideoDeleted @event, CancellationToken cancellationToken = default)
    {
        var video = await _videoRepository.GetAsync(@event.VideoId);
        if (video is null)
        {
            return;
        }

        await _videoRepository.DeleteAsync(video);
    }
}