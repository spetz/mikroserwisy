using VideoHub.Services.Users.Core.Entities;

namespace VideoHub.Services.Users.Core.Repositories;

internal interface IRoleRepository
{
    Task<Role?> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}