using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Users.Core.Events;
using VideoHub.Services.Users.Core.Exceptions;
using VideoHub.Services.Users.Core.Repositories;

namespace VideoHub.Services.Users.Core.Commands.Handlers;

internal sealed class UpdateUserSubscriptionHandler : ICommandHandler<UpdateUserSubscription>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSubscriptionRepository _userSubscriptionRepository;
    private readonly IMessageBroker _messageBroker;

    public UpdateUserSubscriptionHandler(IUserRepository userRepository,
        IUserSubscriptionRepository userSubscriptionRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _userSubscriptionRepository = userSubscriptionRepository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(UpdateUserSubscription command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.UserId);
        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        var subscription = await _userSubscriptionRepository.GetAsync(command.UserId);
        if (subscription is null)
        {
            throw new UserSubscriptionNotFoundException(command.UserId);
        }

        subscription.LengthLimit = command.LengthLimit ?? subscription.LengthLimit;
        subscription.VideosLimit = command.VideosLimit ?? subscription.VideosLimit;
        subscription.SizeLimit = command.SizeLimit ?? subscription.SizeLimit;
        
        if (subscription.LengthLimit < 0 || subscription.VideosLimit < 0 || subscription.SizeLimit < 0)
        {
            throw new InvalidUserSubscriptionLimitsException();
        }
        
        subscription.Version++;
        await _userSubscriptionRepository.UpdateAsync(subscription);
        await _messageBroker.SendAsync(new UserSubscriptionUpdated(subscription.Version, user.Id,
            subscription.SizeLimit, subscription.VideosLimit, subscription.LengthLimit), cancellationToken);
    }
}