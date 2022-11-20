using VideoHub.Services.Users.Core.Entities;

namespace VideoHub.Services.Users.Core.Repositories;

internal interface IUserSubscriptionRepository
{
    Task<UserSubscription?> GetAsync(long userId);
    Task AddAsync(UserSubscription subscription);
    Task UpdateAsync(UserSubscription subscription);
}