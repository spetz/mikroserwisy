using System.Net.Http.Json;
using Micro.HTTP;
using Microsoft.Extensions.Options;
using VideoHub.Services.Videos.Core.Clients;
using VideoHub.Services.Videos.Core.Clients.DTO;

namespace VideoHub.Services.Videos.Infrastructure.Clients;

internal sealed class UsersApiClient : IUsersApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly string _clientName;
    private readonly string _url;

    public UsersApiClient(IHttpClientFactory factory, IOptions<HttpClientOptions> options)
    {
        _factory = factory;
        _clientName = options.Value.Name;
        _url = options.Value.Services["users"];
    }
    
    public async Task<UserSubscriptionDto?> GetUserSubscriptionAsync(long userId)
    {
        var client = _factory.CreateClient(_clientName);
        var response = await client.GetAsync($"{_url}/users/{userId}/subscription");
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<UserSubscriptionDto>();
    }
}