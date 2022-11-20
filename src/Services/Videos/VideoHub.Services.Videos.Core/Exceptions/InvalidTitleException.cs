using Micro.Exceptions;

namespace VideoHub.Services.Videos.Core.Exceptions;

internal sealed class InvalidTitleException : CustomException
{
    public InvalidTitleException() : base("Title is invalid.")
    {
    }
}