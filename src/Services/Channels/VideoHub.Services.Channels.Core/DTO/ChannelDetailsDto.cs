namespace VideoHub.Services.Channels.Core.DTO;

public sealed record ChannelDetailsDto(long ChannelId, string Name, UserDto Owner, string Description,
    IEnumerable<UserDto> Subscribers, IEnumerable<VideoDto> Videos) : ChannelDto(ChannelId, Name, Owner);