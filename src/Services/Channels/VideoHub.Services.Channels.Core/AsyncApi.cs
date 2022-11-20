using Micro.API.AsyncApi;
using Saunter.Attributes;
using VideoHub.Services.Channels.Core.Commands;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Events.External;

namespace VideoHub.Services.Channels.Core;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(channel_added), BindingsRef = "channels")]
    [SubscribeOperation(typeof(ChannelAdded), Summary = "Channel has been created", OperationId = nameof(channel_added))]
    internal abstract void channel_added();
    
    [Channel(nameof(channel_deleted), BindingsRef = "channels")]
    [SubscribeOperation(typeof(ChannelDeleted), Summary = "Channel has been deleted", OperationId = nameof(channel_deleted))]
    internal abstract void channel_deleted();
    
    [Channel(nameof(channel_subscribed), BindingsRef = "channels")]
    [SubscribeOperation(typeof(ChannelSubscribed), Summary = "Channel has been subscribed", OperationId = nameof(channel_subscribed))]
    internal abstract void channel_subscribed();
    
    [Channel(nameof(channel_unsubscribed), BindingsRef = "channels")]
    [SubscribeOperation(typeof(ChannelUnsubscribed), Summary = "Channel has been unsubscribed", OperationId = nameof(channel_unsubscribed))]
    internal abstract void channel_unsubscribed();
    
    [Channel(nameof(signed_up), BindingsRef = "users")]
    [PublishOperation(typeof(SignedUp), Summary = "User has been created", OperationId = nameof(signed_up))]
    internal abstract void signed_up();
    
    [Channel(nameof(video_rendered), BindingsRef = "videos")]
    [PublishOperation(typeof(VideoRendered), Summary = "Video has been rendered", OperationId = nameof(video_rendered))]
    internal abstract void video_rendered();
    
    [Channel(nameof(video_deleted), BindingsRef = "videos")]
    [PublishOperation(typeof(VideoDeleted), Summary = "Video has been deleted", OperationId = nameof(video_deleted))]
    internal abstract void video_deleted();
    
    [Channel(nameof(add_channel), BindingsRef = "channels")]
    [PublishOperation(typeof(AddChannel), Summary = "Add channel", OperationId = nameof(add_channel))]
    internal abstract void add_channel();
}