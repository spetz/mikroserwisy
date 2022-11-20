using Micro.API.AsyncApi;
using Saunter.Attributes;
using VideoHub.Services.Videos.Core.Commands;
using VideoHub.Services.Videos.Core.Events;
using VideoHub.Services.Videos.Core.Events.External;

namespace VideoHub.Services.Videos.Core;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(video_deleted), BindingsRef = "videos")]
    [SubscribeOperation(typeof(VideoDeleted), Summary = "Video has been deleted", OperationId = nameof(video_deleted))]
    internal abstract void video_deleted();
    
    [Channel(nameof(video_render_progressed), BindingsRef = "videos")]
    [SubscribeOperation(typeof(VideoRendered), Summary = "Video has been rendered", OperationId = nameof(video_rendered))]
    internal abstract void video_rendered();
    
    [Channel(nameof(video_received), BindingsRef = "videos")]
    [SubscribeOperation(typeof(VideoReceived), Summary = "Video has been received", OperationId = nameof(video_received))]
    internal abstract void video_received();
    
    [Channel(nameof(VideoRenderProgressed), BindingsRef = "videos")]
    [SubscribeOperation(typeof(VideoRenderProgressed), Summary = "Video render has progressed", OperationId = nameof(video_render_progressed))]
    internal abstract void video_render_progressed();
    
    [Channel(nameof(user_subscription_updated), BindingsRef = "users")]
    [PublishOperation(typeof(UserSubscriptionUpdated), Summary = "User subscription has been updated", OperationId = nameof(user_subscription_updated))]
    internal abstract void user_subscription_updated();
    
    [Channel(nameof(upload_video), BindingsRef = "videos")]
    [PublishOperation(typeof(UploadVideo), Summary = "Upload video", OperationId = nameof(upload_video))]
    internal abstract void upload_video();
}