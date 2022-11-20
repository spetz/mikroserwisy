using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Channels.Core.DTO;
using VideoHub.Services.Channels.Core.Queries;

namespace VideoHub.Services.Channels.Core.DAL.Queries;

internal sealed class GetChannelHandler : IQueryHandler<GetChannel, ChannelDetailsDto?>
{
    private readonly ChannelsDbContext _dbContext;

    public GetChannelHandler(ChannelsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChannelDetailsDto?> HandleAsync(GetChannel query, CancellationToken cancellationToken = default)
    {
        var channel = await _dbContext.Channels
            .AsNoTracking()
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == query.ChannelId, cancellationToken);

        if (channel is null)
        {
            return default;
        }

        var videos = await _dbContext.ChannelVideos
            .AsNoTracking()
            .Where(x => x.ChannelId == query.ChannelId)
            .Include(x => x.Video)
            .Select(x => new VideoDto(x.VideoId, x.Video.Title))
            .ToListAsync(cancellationToken);

        var subscribers = await _dbContext.ChannelSubscriptions
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.ChannelId == query.ChannelId)
            .Select(x => new UserDto(x.UserId, x.User.Username))
            .ToListAsync(cancellationToken);

        return new ChannelDetailsDto(channel.Id, channel.Name, new UserDto(channel.UserId, channel.User.Username),
            channel.Description, subscribers, videos);
    }
}