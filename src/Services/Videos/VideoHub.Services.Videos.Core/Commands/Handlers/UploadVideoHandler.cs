using Micro.Handlers;
using Micro.Messaging.Brokers;
using Micro.Time;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Events;
using VideoHub.Services.Videos.Core.Exceptions;
using VideoHub.Services.Videos.Core.Repositories;
using VideoHub.Services.Videos.Core.Services;

namespace VideoHub.Services.Videos.Core.Commands.Handlers;

internal sealed class UploadVideoHandler : ICommandHandler<UploadVideo>
{
    private static readonly Random Random = new();
    private readonly IVideoRepository _videoRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IVideoRenderer _videoRenderer;
    private readonly IMessageBroker _messageBroker;
    private readonly IClock _clock;

    public UploadVideoHandler(IVideoRepository videoRepository, ISubscriptionRepository subscriptionRepository,
        IVideoRenderer videoRenderer, IMessageBroker messageBroker, IClock clock)
    {
        _videoRepository = videoRepository;
        _subscriptionRepository = subscriptionRepository;
        _videoRenderer = videoRenderer;
        _messageBroker = messageBroker;
        _clock = clock;
    }
    
    public async Task HandleAsync(UploadVideo command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Title) || command.Title.Length > 50)
        {
            throw new InvalidTitleException();
        }
        
        // var subscription = await _usersApiClient.GetUserSubscriptionAsync(command.UserId);
        // if (subscription is null)
        // {
        //     throw new SubscriptionNotFoundException(command.UserId);
        // }

        var subscription = await _subscriptionRepository.GetAsync(command.UserId);
        if (subscription is null)
        {
            throw new SubscriptionNotFoundException(command.UserId);
        }

        var size = GetSize();
        var length = GetLength();
        subscription.ApplyVideo(length, size);

        var video = new Video(command.VideoId, command.UserId, command.Title, size, length, _clock.Current());

        await _subscriptionRepository.UpdateAsync(subscription);
        await _videoRepository.AddAsync(video);
        await _messageBroker.SendAsync(new VideoReceived(command.VideoId, command.UserId, command.Title), cancellationToken);
        await _videoRenderer.RenderAsync(command.VideoId);
    }

    // Calculate the received file size
    private static long GetSize() => Random.Next(1000, 100_000);
    
    // Get the video length in seconds
    private static TimeSpan GetLength() => TimeSpan.FromSeconds(Random.Next(1, 10));
}