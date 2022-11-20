using Micro.Exceptions;

namespace VideoHub.Services.Channels.Core.Exceptions;

internal sealed class ChannelNotFoundException : CustomException
{
    public long ChannelId { get; }

    public ChannelNotFoundException(long channelId) : base($"Channel with ID: {channelId} was not found.")
    {
        ChannelId = channelId;
    }
}