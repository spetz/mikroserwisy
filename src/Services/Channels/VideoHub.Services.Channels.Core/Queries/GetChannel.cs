using Micro.Abstractions;
using VideoHub.Services.Channels.Core.DTO;

namespace VideoHub.Services.Channels.Core.Queries;

public sealed record GetChannel(long ChannelId) : IQuery<ChannelDetailsDto?>;