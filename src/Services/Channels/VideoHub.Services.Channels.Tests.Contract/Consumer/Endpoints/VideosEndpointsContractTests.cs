using System.Diagnostics.CodeAnalysis;
using System.Net;
using Micro.HTTP;
using Micro.Testing;
using Microsoft.Extensions.Options;
using PactNet.Matchers;
using VideoHub.Services.Channels.Core.Clients;
using VideoHub.Services.Channels.Core.Clients.DTO;
using Xunit;
using Xunit.Abstractions;

namespace VideoHub.Services.Channels.Tests.Contract.Consumer.Endpoints;

[ExcludeFromCodeCoverage]
[Collection(Const.TestCollection)]
public class VideosEndpointsContractTests : IDisposable
{
    [Fact]
    public async Task given_valid_request_get_video_should_succeed()
    {
        var dto = new VideoDto(1, 1, "test");

        _endpointContract.Pact
            .UponReceiving("A valid request for get video")
            .WithRequest(HttpMethod.Get, $"/videos/{dto.VideoId}")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithJsonBody(new TypeMatcher(dto));

        await _endpointContract.Pact.VerifyAsync(_ => _apiClient.GetVideoAsync(dto.VideoId));
        // await _endpointContract.PublishToPactBrokerAsync("1");
    }

    #region Arrange

    private readonly EndpointContract _endpointContract;
    private readonly IVideosApiClient _apiClient;

    public VideosEndpointsContractTests(ITestOutputHelper output)
    {
        _endpointContract = new EndpointContract("channels", "videos", output);
        _apiClient = new VideosApiClient(new TestHttpClientFactory(),
            new OptionsWrapper<HttpClientOptions>(new HttpClientOptions
            {
                Services = new Dictionary<string, string>
                {
                    ["videos"] = $"http://localhost:{_endpointContract.Port}"
                }
            }));
    } 

    #endregion

    public void Dispose()
    {
        _endpointContract.Dispose();
    }
}