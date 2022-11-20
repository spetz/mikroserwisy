namespace VideoHub.Services.Users.Core.Clients;

public interface IChannelsApiClient
{
    Task<bool> AddChannelAsync(long userId, string username);
}