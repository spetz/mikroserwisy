using Micro.Handlers;
using Micro.Identity;
using Micro.Messaging.Brokers;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Events.External.Handlers;

internal sealed class SignedUpHandler : IEventHandler<SignedUp>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IUserRepository _userRepository;
    private readonly IIdGen _idGen;
    private readonly IMessageBroker _messageBroker;

    public SignedUpHandler(IChannelRepository channelRepository, IUserRepository userRepository,
        IIdGen idGen, IMessageBroker messageBroker)
    {
        _channelRepository = channelRepository;
        _userRepository = userRepository;
        _idGen = idGen;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        var user = new User(@event.UserId, @event.Username);
        await _userRepository.AddAsync(user);
        var channel = new Channel(_idGen.Create(), @event.UserId, $"{@event.Username} channel");
        await _channelRepository.AddAsync(channel);
        await _messageBroker.SendAsync(new ChannelAdded(channel.Id, channel.UserId, channel.Name), cancellationToken);
    }
}