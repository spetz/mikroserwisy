using Micro.Exceptions;

namespace VideoHub.Services.Videos.Core.Exceptions;

internal sealed class SubscriptionLimitExceededException : CustomException
{
    public long UserId { get; }

    public SubscriptionLimitExceededException(long userId) 
        : base($"Subscription limit exceeded by user with ID: {userId}.")
    {
        UserId = userId;
    }
}