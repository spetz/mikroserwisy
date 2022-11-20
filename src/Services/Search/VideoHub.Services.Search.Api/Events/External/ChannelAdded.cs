using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Search.Api.Events.External;

[Message("channels", "channel_added", "search.channels.channel_added")]
public sealed record ChannelAdded(long ChannelId, string Name) : IEvent;