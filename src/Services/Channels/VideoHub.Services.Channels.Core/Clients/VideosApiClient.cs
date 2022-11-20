using System.Net.Http.Json;
using Micro.HTTP;
using Microsoft.Extensions.Options;
using VideoHub.Services.Channels.Core.Clients.DTO;

namespace VideoHub.Services.Channels.Core.Clients;

internal sealed class VideosApiClient : IVideosApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly string _clientName;
    private readonly string _url;

    public VideosApiClient(IHttpClientFactory factory, IOptions<HttpClientOptions> options)
    {
        _factory = factory;
        _clientName = options.Value.Name;
        _url = options.Value.Services["videos"];
    }
    
    public async Task<VideoDto?> GetVideoAsync(long videoId)
    {
        var client = _factory.CreateClient(_clientName);
        var response = await client.GetAsync($"{_url}/videos/{videoId}");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<VideoDto>();
    }
}