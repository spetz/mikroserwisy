using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class InvalidEmailException : CustomException
{
    public InvalidEmailException() : base("Email is invalid.")
    {
    }
}