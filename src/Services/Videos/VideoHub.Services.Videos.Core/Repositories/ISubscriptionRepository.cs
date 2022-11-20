using VideoHub.Services.Videos.Core.Entities;

namespace VideoHub.Services.Videos.Core.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetAsync(long id);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
}