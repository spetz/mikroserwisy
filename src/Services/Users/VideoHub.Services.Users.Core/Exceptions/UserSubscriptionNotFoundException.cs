using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class UserSubscriptionNotFoundException : CustomException
{
    public long UserId { get; }

    public UserSubscriptionNotFoundException(long userId)
        : base($"Subscription for user with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }
}