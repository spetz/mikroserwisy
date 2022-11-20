using Micro.Exceptions;

namespace VideoHub.Services.Channels.Core.Exceptions;

internal sealed class InvalidChannelNameException : CustomException
{
    public InvalidChannelNameException() : base("Channel name is invalid.")
    {
    }
}