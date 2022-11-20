using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Users.Core.DAL;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries.Handlers;

internal sealed class GetUserSubscriptionHandler : IQueryHandler<GetUserSubscription, UserSubscriptionDto?>
{
    private readonly UsersDbContext _dbContext;

    public GetUserSubscriptionHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserSubscriptionDto?> HandleAsync(GetUserSubscription query, CancellationToken cancellationToken = default)
    {
        var subscription = await _dbContext.UserSubscriptions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == query.UserId, cancellationToken);

        return subscription is null
            ? default
            : new UserSubscriptionDto(subscription.UserId, subscription.SizeLimit, subscription.VideosLimit,
                subscription.LengthLimit);
    }
}