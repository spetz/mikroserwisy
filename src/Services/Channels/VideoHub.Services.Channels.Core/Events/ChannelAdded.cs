using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events;

[Message("channels", "channel_added")]
public sealed record ChannelAdded(long ChannelId, long UserId, string Name) : IEvent;