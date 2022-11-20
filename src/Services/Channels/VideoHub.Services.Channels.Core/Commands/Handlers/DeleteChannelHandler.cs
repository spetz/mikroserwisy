using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class DeleteChannelHandler : ICommandHandler<DeleteChannel>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteChannelHandler(IChannelRepository channelRepository, IMessageBroker messageBroker)
    {
        _channelRepository = channelRepository;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(DeleteChannel command, CancellationToken cancellationToken = default)
    {
        var channel = await _channelRepository.GetAsync(command.ChannelId);
        if (channel is null || channel.UserId != command.UserId)
        {
            throw new ChannelNotFoundException(command.ChannelId);
        }

        await _channelRepository.DeleteAsync(channel);
        await _messageBroker.SendAsync(new ChannelDeleted(command.ChannelId), cancellationToken);
    }
}