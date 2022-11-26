using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using Micro.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using VideoHub.Services.Channels.Core.Commands;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Events;
using Xunit;

namespace VideoHub.Services.Channels.Tests.EndToEnd.Endpoints;

[ExcludeFromCodeCoverage]
[Collection(Const.TestCollection)]
public class AddChannelEndpointTests : IDisposable
{
    [Fact]
    public async Task post_add_channel_should_succeed_and_return_created_status_code()
    {
        var user = new User(1, "user1");
        await _testDatabase.Context.Users.AddAsync(user);
        await _testDatabase.Context.SaveChangesAsync();
        var channelAddedSubscription = _testMessageBroker.SubscribeAsync<ChannelAdded>();
        var command = new AddChannel(default, user.Id, "test");
        _app.Authenticate(user.Id);

        var response = await _app.Client.PostAsJsonAsync("channels", command);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var location = response.Headers.Location;
        location.ShouldNotBeNull();
        var channelAdded = await channelAddedSubscription;
        channelAdded.ShouldNotBeNull();
        var channelId = long.Parse(location.ToString().Split("/").Last());
        var channel = await _testDatabase.Context.Channels.SingleOrDefaultAsync(x => x.Id == channelId);
        // var channel = await _app.Client.GetFromJsonAsync<ChannelDetailsDto>(location);
        channel.ShouldNotBeNull();
    }

    #region Arrange
    
    private readonly TestDatabase _testDatabase;
    private readonly TestMessageBroker _testMessageBroker;
    private readonly TestApp<Program> _app;

    public AddChannelEndpointTests()
    {
        _testDatabase = new TestDatabase();
        _testMessageBroker = new TestMessageBroker();
        _app = new TestApp<Program>();
    }
    
    #endregion
    
    public void Dispose()
    {
        _testDatabase.Dispose();
        _testMessageBroker.Dispose();
    }
}