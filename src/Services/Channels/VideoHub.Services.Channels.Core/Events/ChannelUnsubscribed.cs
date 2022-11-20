using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events;

[Message("channels", "channel_unsubscribed")]
public sealed record ChannelUnsubscribed(long ChannelId, long UserId) : IEvent;