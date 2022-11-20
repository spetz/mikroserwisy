using Micro.Handlers;

namespace VideoHub.Services.Notifications.Api.Events.External.Handlers;

internal sealed class VideoRenderedHandler : IEventHandler<VideoRendered>
{
    private readonly ILogger<VideoRenderedHandler> _logger;

    public VideoRenderedHandler(ILogger<VideoRenderedHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleAsync(VideoRendered @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Video: {@event.VideoId} rendered.");
        await Task.CompletedTask;
    }
}