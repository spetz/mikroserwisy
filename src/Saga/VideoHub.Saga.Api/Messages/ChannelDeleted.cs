using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("channels", "channel_deleted", "saga.channels.channel_deleted")]
public sealed record ChannelDeleted(long ChannelId) : IEvent;