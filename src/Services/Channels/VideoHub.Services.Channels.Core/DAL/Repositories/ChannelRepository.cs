using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.Entities;
using VideoHub.Services.Channels.Core.Repositories;

namespace VideoHub.Services.Channels.Core.DAL.Repositories;

internal sealed class ChannelRepository : IChannelRepository
{
    private readonly ChannelsDbContext _dbContext;

    public ChannelRepository(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Channel?> GetAsync(long id)
        => _dbContext.Channels.SingleOrDefaultAsync(x => x.Id == id);
    
    public Task<Channel?> GetAsync(string name)
        => _dbContext.Channels.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Channel channel)
    {
        await _dbContext.Channels.AddAsync(channel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Channel channel)
    {
        _dbContext.Channels.Remove(channel);
        await _dbContext.SaveChangesAsync();
    }
}