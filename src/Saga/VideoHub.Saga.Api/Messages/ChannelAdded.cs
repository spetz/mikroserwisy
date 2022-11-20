using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("channels", "channel_added", "saga.channels.channel_added")]
public sealed record ChannelAdded(long ChannelId, long UserId) : IEvent;