using System.Diagnostics;
using Micro.Messaging.Brokers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Events;
using VideoHub.Services.Videos.Core.Repositories;

namespace VideoHub.Services.Videos.Infrastructure.Renderers;

internal sealed class VideoRenderer
{
    private static readonly Random Random = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<VideoRenderer> _logger;

    public VideoRenderer(IServiceProvider serviceProvider, IMessageBroker messageBroker, ILogger<VideoRenderer> logger)
    {
        _serviceProvider = serviceProvider;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task ProcessAsync(long videoId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var videoRepository = scope.ServiceProvider.GetRequiredService<IVideoRepository>();
            var video = await videoRepository.GetAsync(videoId);
            video!.SetState(VideoState.Processing);
            await videoRepository.UpdateAsync(video);
        }
        
        _logger.LogInformation($"Started rendering a video with ID: {videoId}'...");
        
        var stopwatch = new Stopwatch();
        var renderTime = Random.Next(3, 30);
        var second = TimeSpan.FromSeconds(1);
        var step = 100 / renderTime;
        var progress = 0;
        stopwatch.Start();
        for (var i = 0; i < renderTime; i++)
        {
            // Do the actual work :)
            await Task.Delay(second);
            progress += step;
            await _messageBroker.SendAsync(new VideoRenderProgressed(videoId, progress));
        }

        stopwatch.Stop();
        
        using (var scope = _serviceProvider.CreateScope())
        {
            var videoRepository = scope.ServiceProvider.GetRequiredService<IVideoRepository>();
            var video = await videoRepository.GetAsync(videoId);
            video!.SetState(VideoState.Rendered);
            await videoRepository.UpdateAsync(video);
            await _messageBroker.SendAsync(new VideoRendered(videoId, video.UserId, video.Title));
        }
        
        _logger.LogInformation($"Completed rendering a video with ID: '{videoId}' in {stopwatch.Elapsed}.");
    }
}