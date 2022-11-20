using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("channels", "channel_subscribed", "saga.channels.channel_subscribed")]
public sealed record ChannelSubscribed(long ChannelId) : IEvent;