using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}