using Micro.Exceptions;

namespace VideoHub.Services.Videos.Core.Exceptions;

internal sealed class VideoNotFoundException : CustomException
{
    public long VideoId { get; }

    public VideoNotFoundException(long videoId) : base($"Video with ID: {videoId} was not found.")
    {
        VideoId = videoId;
    }
}