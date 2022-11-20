using Micro.Handlers;
using VideoHub.Services.Search.Api.Services;

namespace VideoHub.Services.Search.Api.Events.External.Handlers;

internal sealed class VideoDeletedHandler : IEventHandler<VideoDeleted>
{
    private readonly ISearchService _searchService;

    public VideoDeletedHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Task HandleAsync(VideoDeleted @event, CancellationToken cancellationToken = default)
        => _searchService.DeleteAsync(@event.VideoId);
}