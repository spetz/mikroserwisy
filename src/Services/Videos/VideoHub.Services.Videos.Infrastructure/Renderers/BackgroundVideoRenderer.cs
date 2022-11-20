using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VideoHub.Services.Videos.Infrastructure.Channels;

namespace VideoHub.Services.Videos.Infrastructure.Renderers;

internal sealed class BackgroundVideoRenderer : BackgroundService
{
    private readonly VideoRendererChannel _rendererChannel;
    private readonly VideoRenderer _renderer;
    private readonly ILogger<BackgroundVideoRenderer> _logger;

    public BackgroundVideoRenderer(VideoRendererChannel rendererChannel, VideoRenderer renderer,
        ILogger<BackgroundVideoRenderer> logger)
    {
        _rendererChannel = rendererChannel;
        _renderer = renderer;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in _rendererChannel.SubscribeAsync(stoppingToken))
        {
            try
            {
                await _renderer.ProcessAsync(command);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}