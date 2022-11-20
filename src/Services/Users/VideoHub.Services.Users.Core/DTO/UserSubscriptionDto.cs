namespace VideoHub.Services.Users.Core.DTO;

public record UserSubscriptionDto(long UserId, long SizeLimit, long VideosLimit, long LengthLimit);