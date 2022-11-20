using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class UsernameInUseException : CustomException
{
    public string Username { get; }

    public UsernameInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}