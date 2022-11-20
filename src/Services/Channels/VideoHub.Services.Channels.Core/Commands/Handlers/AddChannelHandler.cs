using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Exceptions;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.Commands.Handlers;

internal sealed class AddChannelHandler : ICommandHandler<AddChannel>
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageBroker _messageBroker;

    public AddChannelHandler(IChannelRepository channelRepository, IMessageBroker messageBroker)
    {
        _channelRepository = channelRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(AddChannel command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length > 50)
        {
            throw new InvalidChannelNameException();
        }
        
        if (!string.IsNullOrWhiteSpace(command.Description) && command.Description.Length > 500)
        {
            throw new InvalidChannelDescription();
        }
        
        var channel = await _channelRepository.GetAsync(command.Name);
        if (channel is not null)
        {
            throw new ChannelNameAlreadyInUseException(command.Name);
        }
        
        channel = new Channel(command.ChannelId, command.UserId, command.Name, command.Description);
        await _channelRepository.AddAsync(channel);
        await _messageBroker.SendAsync(new ChannelAdded(command.ChannelId, command.UserId, command.Name),
            cancellationToken);
    }
}