using Micro.Abstractions;
using VideoHub.Services.Channels.Core.DTO;

namespace VideoHub.Services.Channels.Core.Queries;

public sealed record GetChannels(string? Name = default, long? UserId = default) : IQuery<IEnumerable<ChannelDto>>;