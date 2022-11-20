using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class InvalidUserSubscriptionLimitsException : CustomException
{
    public InvalidUserSubscriptionLimitsException() : base("User subscription limits are invalid.")
    {
    }
}