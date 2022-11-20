using Micro.Handlers;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Events.External.Handlers;

internal sealed class VideoRenderedHandler : IEventHandler<VideoRendered>
{
    private readonly IVideoRepository _videoRepository;

    public VideoRenderedHandler(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }
    
    public async Task HandleAsync(VideoRendered @event, CancellationToken cancellationToken = default)
    {
        var video = new Video(@event.VideoId, @event.UserId, @event.Title);
        await _videoRepository.AddAsync(video);
    }
}