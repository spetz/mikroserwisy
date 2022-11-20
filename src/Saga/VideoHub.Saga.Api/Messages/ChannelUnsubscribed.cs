using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("channels", "channel_unsubscribed", "saga.channels.channel_unsubscribed")]
public sealed record ChannelUnsubscribed(long ChannelId) : IEvent;