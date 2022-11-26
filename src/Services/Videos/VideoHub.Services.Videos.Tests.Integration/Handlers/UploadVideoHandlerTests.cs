using Micro.Handlers;
using Micro.Messaging.Brokers;
using Micro.Testing;
using Micro.Time;
using Shouldly;
using VideoHub.Services.Videos.Core.Commands;
using VideoHub.Services.Videos.Core.Commands.Handlers;
using VideoHub.Services.Videos.Core.Entities;
using VideoHub.Services.Videos.Core.Events;
using VideoHub.Services.Videos.Core.Repositories;
using VideoHub.Services.Videos.Core.Services;
using VideoHub.Services.Videos.Infrastructure.Channels;
using VideoHub.Services.Videos.Infrastructure.DAL.Repositories;
using Xunit;

namespace VideoHub.Services.Videos.Tests.Integration.Handlers;

public class UploadVideoHandlerTests : IDisposable
{
    private Task Act(UploadVideo command) => _handler.HandleAsync(command);

    [Fact]
    public async Task given_valid_command_upload_video_should_succeed()
    {
        // Arrange
        await _testDatabase.InitAsync();
        var subscription = new Subscription(1, int.MaxValue, int.MaxValue, 1);
        await _subscriptionRepository.AddAsync(subscription);
        var command = new UploadVideo(1, subscription.UserId, "Test video");
        var videoReceivedSubscription = _testMessageBroker.SubscribeAsync<VideoReceived>();

        // Act
        await Act(command);

        // Assert
        var video = await _videoRepository.GetAsync(command.VideoId);
        video.ShouldNotBeNull();
        var videoReceived = await videoReceivedSubscription;
        videoReceived.ShouldNotBeNull();
    }

    #region Arrange

    private readonly TestDatabase _testDatabase;
    private readonly TestMessageBroker _testMessageBroker;
    private readonly IVideoRepository _videoRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IVideoRenderer _videoRenderer;
    private readonly IClock _clock;
    private readonly ICommandHandler<UploadVideo> _handler;
    
    
    public UploadVideoHandlerTests()
    {
        _testDatabase = new TestDatabase();
        _testMessageBroker = new TestMessageBroker();
        _videoRepository = new VideoRepository(_testDatabase.Context);
        _subscriptionRepository = new SubscriptionRepository(_testDatabase.Context);
        _videoRenderer = new VideoRendererChannel();
        _clock = new UtcClock();
        _handler = new UploadVideoHandler(_videoRepository, _subscriptionRepository,
            _videoRenderer, _testMessageBroker.MessageBroker, _clock);
    }

    #endregion

    public void Dispose()
    {
        _testDatabase.Dispose();
        _testMessageBroker.Dispose();
    }
}