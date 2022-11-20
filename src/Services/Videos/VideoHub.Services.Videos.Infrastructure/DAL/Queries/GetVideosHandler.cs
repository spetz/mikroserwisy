using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Core.DTO;
using VideoHub.Services.Videos.Core.Queries;

namespace VideoHub.Services.Videos.Infrastructure.DAL.Queries;

internal sealed class GetVideosHandler : IQueryHandler<GetVideos, IEnumerable<VideoDto>>
{
    private readonly VideosDbContext _dbContext;

    public GetVideosHandler(VideosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<VideoDto>> HandleAsync(GetVideos query, CancellationToken cancellationToken = default)
        => await _dbContext.Videos
            .AsNoTracking()
            .Where(x => ((query.Title != null && x.Title.Contains(query.Title)) || query.Title == null) &&
                        ((query.UserId != null && x.UserId == query.UserId) || query.UserId == null))
            .Select(x => new VideoDto(x.Id, x.UserId, x.Title))
            .ToListAsync(cancellationToken);
}