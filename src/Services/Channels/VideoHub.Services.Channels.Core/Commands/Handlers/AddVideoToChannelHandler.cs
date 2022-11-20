using Micro.Handlers;
using VideoHub.Services.Channels.Core.Clients;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class AddVideoToChannelHandler : ICommandHandler<AddVideoToChannel>
{
    private const string RenderedVideoState = "rendered";
    private readonly IChannelRepository _channelRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IChannelVideoRepository _channelVideoRepository;
    private readonly IVideosApiClient _videosApiClient;

    public AddVideoToChannelHandler(IChannelRepository channelRepository, IVideoRepository videoRepository,
        IChannelVideoRepository channelVideoRepository, IVideosApiClient videosApiClient)
    {
        _channelRepository = channelRepository;
        _videoRepository = videoRepository;
        _channelVideoRepository = channelVideoRepository;
        _videosApiClient = videosApiClient;
    }

    public async Task HandleAsync(AddVideoToChannel command, CancellationToken cancellationToken = default)
    {
        var channel = await _channelRepository.GetAsync(command.ChannelId);
        if (channel is null || channel.UserId != command.UserId)
        {
            throw new ChannelNotFoundException(command.ChannelId);
        }

        // var video = await _videosApiClient.GetVideoAsync(command.VideoId);
        // if (video is null || video.UserId != command.UserId || video.State is not RenderedVideoState)
        // {
        //     throw new VideoNotFoundException(command.VideoId);
        // }
        
        var video = await _videoRepository.GetAsync(command.VideoId);
        if (video is null || video.UserId != command.UserId)
        {
            throw new VideoNotFoundException(command.VideoId);
        }

        var channelVideo = await _channelVideoRepository.GetAsync(command.ChannelId, command.VideoId);
        if (channelVideo is not null)
        {
            return;
        }

        channelVideo = new ChannelVideo(command.ChannelId, command.VideoId);
        await _channelVideoRepository.AddAsync(channelVideo);
    }
}