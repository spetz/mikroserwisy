using Micro.Handlers;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Repositories;

namespace VideoHub.Services.Videos.Core.Events.External.Handlers;

internal sealed class UserSubscriptionUpdatedHandler : IEventHandler<UserSubscriptionUpdated>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public UserSubscriptionUpdatedHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task HandleAsync(UserSubscriptionUpdated @event, CancellationToken cancellationToken = default)
    {
        var subscription = await _subscriptionRepository.GetAsync(@event.UserId);
        if (subscription is null)
        {
            await _subscriptionRepository.AddAsync(new Subscription(@event.UserId, @event.SizeLimit,
                @event.LengthLimit, @event.VideosLimit, @event.Version));
            return;
        }

        if (subscription.Version >= @event.Version)
        {
            return;
        }

        subscription.UpdateLimits(@event.SizeLimit, @event.LengthLimit, @event.VideosLimit, @event.Version);
        await _subscriptionRepository.UpdateAsync(subscription);
    }
}