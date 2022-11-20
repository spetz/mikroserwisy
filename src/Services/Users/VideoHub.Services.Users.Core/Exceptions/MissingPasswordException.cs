using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class MissingPasswordException : CustomException
{
    public MissingPasswordException() : base($"Invalid password")
    {
    }
}