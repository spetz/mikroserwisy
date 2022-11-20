using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class UserNotFoundException : CustomException
{
    public long UserId { get; }

    public UserNotFoundException(long userId) : base($"User with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }
}