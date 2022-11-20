using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Search.Api.Events.External;

[Message("channels", "channel_deleted", "search.channels.channel_deleted")]
public sealed record ChannelDeleted(long ChannelId) : IEvent;