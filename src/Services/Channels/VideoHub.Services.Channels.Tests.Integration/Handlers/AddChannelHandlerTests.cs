using System.Diagnostics.CodeAnalysis;
using Micro.Handlers;
using Micro.Testing;
using Shouldly;
using VideoHub.Services.Channels.Core.Commands;
using VideoHub.Services.Channels.Core.Commands.Handlers;
using VideoHub.Services.Channels.Core.DAL.Repositories;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Events;
using VideoHub.Services.Channels.Core.Repositories;
using Xunit;

namespace VideoHub.Services.Channels.Tests.Integration.Handlers;

[ExcludeFromCodeCoverage]
public class AddChannelHandlerTests : IDisposable
{
    private Task Act(AddChannel command) => _handler.HandleAsync(command);

    [Fact]
    public async Task given_valid_command_add_channel_should_succeed_and_publish_an_event()
    {
        await _testDatabase.InitAsync();
        var user = new User(1, "user1");
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();
        var channelAddedSubscription = _testMessageBroker.SubscribeAsync<ChannelAdded>();
        var command = new AddChannel(1, user.Id, "test");

        await Act(command);

        var channel = await _channelRepository.GetAsync(command.ChannelId);
        channel.ShouldNotBeNull();
        var channelAdded = await channelAddedSubscription;
        channelAdded.ShouldNotBeNull();
    }

    #region Arrange

    private readonly TestDatabase _testDatabase;
    private readonly TestMessageBroker _testMessageBroker;
    private readonly IChannelRepository _channelRepository;
    private readonly ICommandHandler<AddChannel> _handler;

    public AddChannelHandlerTests()
    {
        _testDatabase = new TestDatabase();
        _testMessageBroker = new TestMessageBroker();
        _channelRepository = new ChannelRepository(_testDatabase.Context);
        _handler = new AddChannelHandler(_channelRepository, _testMessageBroker.MessageBroker);
    }

    #endregion
    
    public void Dispose()
    {
        _testDatabase.Dispose();
        _testMessageBroker.Dispose();
    }
}