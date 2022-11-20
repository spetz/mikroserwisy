using Micro.Handlers;
using Microsoft.EntityFrameworkCore;
using VideoHub.Services.Videos.Core.DTO;
using VideoHub.Services.Videos.Core.Queries;

namespace VideoHub.Services.Videos.Infrastructure.DAL.Queries;

internal sealed class GetVideoHandler : IQueryHandler<GetVideo, VideoDetailsDto?>
{
    private readonly VideosDbContext _dbContext;

    public GetVideoHandler(VideosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VideoDetailsDto?> HandleAsync(GetVideo query, CancellationToken cancellationToken = default)
    {
        var video = await _dbContext.Videos
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.VideoId, cancellationToken);

        return video is null
            ? default
            : new VideoDetailsDto(video.Id, video.UserId, video.Title, TimeSpan.FromSeconds(video.Length),
                video.State.ToString().ToLowerInvariant());
    }
}