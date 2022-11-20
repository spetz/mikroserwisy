using Micro.Handlers;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class DeleteVideoFromChannelHandler : ICommandHandler<DeleteVideoFromChannel>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IChannelVideoRepository _channelVideoRepository;

    public DeleteVideoFromChannelHandler(IChannelRepository channelRepository, IVideoRepository videoRepository,
        IChannelVideoRepository channelVideoRepository)
    {
        _channelRepository = channelRepository;
        _videoRepository = videoRepository;
        _channelVideoRepository = channelVideoRepository;
    }

    public async Task HandleAsync(DeleteVideoFromChannel command, CancellationToken cancellationToken = default)
    {
        var channel = await _channelRepository.GetAsync(command.ChannelId);
        if (channel is null || channel.UserId != command.UserId)
        {
            throw new ChannelNotFoundException(command.ChannelId);
        }

        var channelVideo = await _channelVideoRepository.GetAsync(command.ChannelId, command.VideoId);
        if (channelVideo is null)
        {
            return;
        }

        await _channelVideoRepository.DeleteAsync(channelVideo);
    }
}