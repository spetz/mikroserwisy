using Micro.Exceptions;

namespace VideoHub.Services.Channels.Core.Exceptions;

internal sealed class InvalidChannelDescription : CustomException
{
    public InvalidChannelDescription() : base("Channel description is invalid.")
    {
    }
}