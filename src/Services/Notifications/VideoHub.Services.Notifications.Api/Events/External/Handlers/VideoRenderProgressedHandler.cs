using Micro.Handlers;

namespace VideoHub.Services.Notifications.Api.Events.External.Handlers;

internal sealed class VideoRenderProgressedHandler : IEventHandler<VideoRenderProgressed>
{
    private readonly ILogger<VideoRenderProgressedHandler> _logger;

    public VideoRenderProgressedHandler(ILogger<VideoRenderProgressedHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleAsync(VideoRenderProgressed @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Video: {@event.VideoId} render progressed: {@event.Progress}%");
        await Task.CompletedTask;
    }
}