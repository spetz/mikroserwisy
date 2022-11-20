namespace VideoHub.Services.Videos.Core.DTO;

public sealed record VideoDetailsDto(long VideoId, long UserId, string Title, TimeSpan Length, string State) : VideoDto(VideoId, UserId, Title);