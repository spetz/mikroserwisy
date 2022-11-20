using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events;

[Message("channels", "channel_subscribed")]
public sealed record ChannelSubscribed(long ChannelId, long UserId) : IEvent;