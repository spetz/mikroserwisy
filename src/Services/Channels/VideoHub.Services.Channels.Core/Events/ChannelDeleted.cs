using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events;

[Message("channels", "channel_deleted")]
public sealed record ChannelDeleted(long ChannelId) : IEvent;