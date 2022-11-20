using System.Net.Http.Json;
using Micro.HTTP;
using Microsoft.Extensions.Options;

namespace VideoHub.Services.Users.Core.Clients;

internal sealed class ChannelsApiClient : IChannelsApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly string _clientName;
    private readonly string _url;

    public ChannelsApiClient(IHttpClientFactory factory, IOptions<HttpClientOptions> options)
    {
        _factory = factory;
        _clientName = options.Value.Name;
        _url = options.Value.Services["channels"];
    }

    public async Task<bool> AddChannelAsync(long userId, string username)
    {
        var client = _factory.CreateClient(_clientName);
        var payload = new AddChanel(userId, $"{username} channel");
        var response = await client.PostAsJsonAsync($"{_url}/channels", payload);
        
        return response.IsSuccessStatusCode;
    }

    private sealed record AddChanel(long UserId, string Name);
}