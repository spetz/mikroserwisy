using System.Net;
using Shouldly;
using Xunit;

namespace VideoHub.Services.Videos.Tests.EndToEnd.Endpoints;

public class HomeEndpointTests
{
    [Fact]
    public async Task get_base_endpoint_should_return_ok_status_code()
    {
        // Arrange
        var api = new TestVideosApi();

        // Act
        var response = await api.Client.GetAsync("");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}