using VideoHub.Services.Videos.Core.Clients.DTO;

namespace VideoHub.Services.Videos.Core.Clients;

public interface IUsersApiClient
{
    Task<UserSubscriptionDto?> GetUserSubscriptionAsync(long userId);
}