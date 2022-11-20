using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class UnsubscribeChannelHandler : ICommandHandler<UnsubscribeChannel>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IChannelSubscriptionRepository _channelSubscriptionRepository;
    private readonly IMessageBroker _messageBroker;

    public UnsubscribeChannelHandler(IChannelRepository channelRepository,
        IChannelSubscriptionRepository channelSubscriptionRepository, IMessageBroker messageBroker)
    {
        _channelRepository = channelRepository;
        _channelSubscriptionRepository = channelSubscriptionRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(UnsubscribeChannel command, CancellationToken cancellationToken = default)
    {
        var channel = await _channelRepository.GetAsync(command.ChannelId);
        if (channel is null)
        {
            throw new ChannelNotFoundException(command.ChannelId);
        }

        var subscription = await _channelSubscriptionRepository.GetAsync(command.ChannelId, command.UserId);
        if (subscription is null)
        {
            return;
        }

        await _channelSubscriptionRepository.DeleteAsync(subscription);
        await _messageBroker.SendAsync(new ChannelUnsubscribed(command.ChannelId, command.UserId), cancellationToken);
    }
}