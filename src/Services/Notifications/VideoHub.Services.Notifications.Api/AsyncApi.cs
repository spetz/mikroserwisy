using Micro.API.AsyncApi;
using Saunter.Attributes;
using VideoHub.Services.Notifications.Api.Events.External;

namespace VideoHub.Services.Notifications.Api;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(VideoRenderProgressed), BindingsRef = "videos")]
    [PublishOperation(typeof(VideoRenderProgressed), Summary = "Video render has progressed", OperationId = nameof(video_render_progressed))]
    internal abstract void video_render_progressed();
    
    [Channel(nameof(VideoRendered), BindingsRef = "videos")]
    [PublishOperation(typeof(VideoRendered), Summary = "Video has been rendered", OperationId = nameof(video_rendered))]
    internal abstract void video_rendered();
}