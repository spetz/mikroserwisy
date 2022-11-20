using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Repositories;

namespace VideoHub.Services.Videos.Infrastructure.DAL.Repositories;

internal sealed class SubscriptionRepository : ISubscriptionRepository
{
    private readonly VideosDbContext _dbContext;

    public SubscriptionRepository(VideosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Subscription?> GetAsync(long id)
        => _dbContext.Subscriptions.SingleOrDefaultAsync(x => x.UserId == id);

    public async Task AddAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Subscriptions.Update(subscription);
        await _dbContext.SaveChangesAsync();
    }
}