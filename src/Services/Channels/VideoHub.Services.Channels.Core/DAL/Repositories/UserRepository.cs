using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ChannelsDbContext _dbContext;

    public UserRepository(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetAsync(long id)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}