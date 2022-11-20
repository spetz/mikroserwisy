using Micro.Exceptions;

namespace VideoHub.Services.Videos.Core.Exceptions;

internal sealed class SubscriptionNotFoundException : CustomException
{
    public long UserId { get; }

    public SubscriptionNotFoundException(long userId)
        : base($"Subscription for user with ID: {userId} was not found.")
    {
        UserId = userId;
    }
}