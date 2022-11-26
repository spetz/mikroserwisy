using System.Net;
using System.Net.Http.Json;
using Micro.Testing;
using Shouldly;
using VideoHub.Services.Videos.Core.Commands;
using VideoHub.Services.Videos.Core.Entities;
using Xunit;

namespace VideoHub.Services.Videos.Tests.EndToEnd.Endpoints;

[Collection("upload-video")]
public class UploadVideoEndpointsTests : IDisposable
{
    [Fact]
    public async Task post_upload_video_given_authenticated_user_should_succeed()
    {
        var subscription = new Subscription(1, int.MaxValue, int.MaxValue, 1);
        await _testDatabase.Context.Subscriptions.AddAsync(subscription);
        await _testDatabase.Context.SaveChangesAsync();
        _app.Authenticate(subscription.UserId);
        var command = new UploadVideo(default, subscription.UserId, "Test video");

        var response = await _app.Client.PostAsJsonAsync("videos", command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    private readonly TestDatabase _testDatabase;
    private readonly TestApp<Program> _app;

    public UploadVideoEndpointsTests()
    {
        _app = new TestApp<Program>();
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _app.Dispose();
        _testDatabase.Dispose();
    }
}