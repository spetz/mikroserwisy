using Micro.Exceptions;

namespace VideoHub.Services.Channels.Core.Exceptions;

internal sealed class ChannelNameAlreadyInUseException : CustomException
{
    public string Name { get; }

    public ChannelNameAlreadyInUseException(string name) : base($"Channel name: '{name}' is already in use.")
    {
        Name = name;
    }
}