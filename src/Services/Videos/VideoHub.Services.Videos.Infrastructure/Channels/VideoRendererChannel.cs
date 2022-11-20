using System.Threading.Channels;
using VideoHub.Services.Videos.Core.Services;

namespace VideoHub.Services.Videos.Infrastructure.Channels;

internal sealed class VideoRendererChannel : IVideoRenderer
{
    private readonly Channel<long> _videos = Channel.CreateUnbounded<long>();
    
    public async Task RenderAsync(long videoId) => await _videos.Writer.WriteAsync(videoId);
    
    public IAsyncEnumerable<long> SubscribeAsync(CancellationToken cancellationToken)
        => _videos.Reader.ReadAllAsync(cancellationToken);
}