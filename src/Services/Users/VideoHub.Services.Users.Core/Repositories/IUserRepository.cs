using VideoHub.Services.Users.Core.Entities;

namespace VideoHub.Services.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User?> GetAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}