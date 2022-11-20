using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class InvalidUsernameException : CustomException
{
    public InvalidUsernameException() : base("Username is invalid.")
    {
    }
}