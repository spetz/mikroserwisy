namespace VideoHub.Services.Channels.Core.Entities;

public class ChannelVideo
{
    public long ChannelId { get; private set; }
    public Channel Channel { get; private set; } = null!;
    public long VideoId { get; private set; }
    public Video Video { get; private set; }= null!;

    private ChannelVideo()
    {
    }

    public ChannelVideo(long channelId, long videoId)
    {
        ChannelId = channelId;
        VideoId = videoId;
    }
}