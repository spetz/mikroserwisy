namespace VideoHub.Services.Channels.Core.Entities;

public class ChannelSubscription
{
    public long ChannelId { get; private set; }
    public Channel Channel { get; private set; } = null!;
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    private ChannelSubscription()
    {
    }

    public ChannelSubscription(long channelId, long userId)
    {
        ChannelId = channelId;
        UserId = userId;
    }
}