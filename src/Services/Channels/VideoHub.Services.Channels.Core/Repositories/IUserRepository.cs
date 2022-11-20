using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(long id);
    Task AddAsync(User user);
}