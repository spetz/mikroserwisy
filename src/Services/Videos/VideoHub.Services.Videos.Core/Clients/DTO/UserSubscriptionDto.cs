namespace VideoHub.Services.Videos.Core.Clients.DTO;

public record UserSubscriptionDto(long UserId, long SizeLimit, long VideosLimit, long LengthLimit);