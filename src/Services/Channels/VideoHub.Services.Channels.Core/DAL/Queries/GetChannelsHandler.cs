using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.DTO;
using VideoHub.Services.Channels.Core.Queries;

namespace VideoHub.Services.Channels.Core.DAL.Queries;

internal sealed class GetChannelsHandler : IQueryHandler<GetChannels, IEnumerable<ChannelDto>>
{
    private readonly ChannelsDbContext _dbContext;

    public GetChannelsHandler(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ChannelDto>> HandleAsync(GetChannels query,
        CancellationToken cancellationToken = default)
        => await _dbContext.Channels
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => ((query.Name != null && x.Name.Contains(query.Name)) || query.Name == null) &&
                        ((query.UserId != null && x.UserId == query.UserId) || query.UserId == null))
            .Select(x => new ChannelDto(x.Id, x.Name, new UserDto(x.UserId, x.User.Username)))
            .ToListAsync(cancellationToken);
}