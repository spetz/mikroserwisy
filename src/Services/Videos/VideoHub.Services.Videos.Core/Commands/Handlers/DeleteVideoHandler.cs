using Micro.Handlers;
using Micro.Messaging.Brokers;
using VideoHub.Services.Videos.Core.Events;
using VideoHub.Services.Videos.Core.Exceptions;
using VideoHub.Services.Videos.Core.Repositories;

namespace VideoHub.Services.Videos.Core.Commands.Handlers;

internal sealed class DeleteVideoHandler : ICommandHandler<DeleteVideo>
{
    private readonly IVideoRepository _videoRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteVideoHandler(IVideoRepository videoRepository, ISubscriptionRepository subscriptionRepository,
        IMessageBroker messageBroker)
    {
        _videoRepository = videoRepository;
        _subscriptionRepository = subscriptionRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(DeleteVideo command, CancellationToken cancellationToken = default)
    {
        var video = await _videoRepository.GetAsync(command.VideoId);
        if (video is null || video.UserId != command.UserId)
        {
            throw new VideoNotFoundException(command.UserId);
        }
        
        var subscription = await _subscriptionRepository.GetAsync(command.UserId);
        if (subscription is null)
        {
            throw new SubscriptionNotFoundException(command.UserId);
        }
        
        subscription.RemoveVideo(video.Size);
        await _videoRepository.DeleteAsync(video);
        await _messageBroker.SendAsync(new VideoDeleted(command.VideoId), cancellationToken);
    }
}