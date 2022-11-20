using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class SubscribeChannelHandler : ICommandHandler<SubscribeChannel>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IChannelSubscriptionRepository _channelSubscriptionRepository;
    private readonly IMessageBroker _messageBroker;

    public SubscribeChannelHandler(IChannelRepository channelRepository,
        IChannelSubscriptionRepository channelSubscriptionRepository, IMessageBroker messageBroker)
    {
        _channelRepository = channelRepository;
        _channelSubscriptionRepository = channelSubscriptionRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(SubscribeChannel command, CancellationToken cancellationToken = default)
    {
        var channel = await _channelRepository.GetAsync(command.ChannelId);
        if (channel is null)
        {
            throw new ChannelNotFoundException(command.ChannelId);
        }

        var subscription = await _channelSubscriptionRepository.GetAsync(command.ChannelId, command.UserId);
        if (subscription is not null)
        {
            return;
        }

        subscription = new ChannelSubscription(command.ChannelId, command.UserId);
        await _channelSubscriptionRepository.AddAsync(subscription);
        await _messageBroker.SendAsync(new ChannelSubscribed(command.ChannelId, command.UserId), cancellationToken);
    }
}