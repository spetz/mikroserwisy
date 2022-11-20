using VideoHub.Services.Videos.Core.Exceptions;

namespace VideoHub.Services.Videos.Core.Entities;

public class Subscription
{
    public long UserId { get; private set; }
    public long SizeLimit { get; private set; }
    public long LengthLimit { get; private set; }
    public long VideosLimit { get; private set; }
    public long UsedSizeLimit { get; private set; }
    public long UsedVideosLimit { get; private set; }
    public int Version { get; private set; }

    private Subscription()
    {
    }

    public Subscription(long userId, long sizeLimit, long lengthLimit, long videosLimit, int version = 1)
    {
        UserId = userId;
        SizeLimit = sizeLimit;
        LengthLimit = lengthLimit;
        VideosLimit = videosLimit;
        SetVersion(version);
    }
    
    public void UpdateLimits(long sizeLimit, long lengthLimit, long videosLimit, int version)
    {
        SizeLimit = sizeLimit;
        LengthLimit = lengthLimit;
        VideosLimit = videosLimit;
        SetVersion(version);
    }

    public void ApplyVideo(TimeSpan length, long size)
    {
        var videosUsage = UsedVideosLimit + 1;
        if (videosUsage > VideosLimit)
        {
            throw new SubscriptionLimitExceededException(UserId);
        }

        if (length.TotalMinutes > LengthLimit)
        {
            throw new SubscriptionLimitExceededException(UserId);
        }

        var sizeUsage = UsedSizeLimit + size;
        if (sizeUsage > SizeLimit)
        {
            throw new SubscriptionLimitExceededException(UserId);
        }

        UsedSizeLimit = sizeUsage;
        UsedVideosLimit = videosUsage;
        SetVersion(Version + 1);
    }

    public void RemoveVideo(long size)
    {
        var videosLimit = UsedVideosLimit - 1;
        var sizeLimit = UsedSizeLimit - size;
        UsedVideosLimit = videosLimit < 0 ? 0 : videosLimit;
        UsedSizeLimit = sizeLimit < 0 ? 0 : sizeLimit;
        SetVersion(Version + 1);
    }
    
    private void SetVersion(int version) => Version = version;
}