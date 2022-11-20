using Micro.Handlers;
using VideoHub.Services.Search.Api.Models;
using VideoHub.Services.Search.Api.Services;

namespace VideoHub.Services.Search.Api.Events.External.Handlers;

internal sealed class VideoRenderedHandler : IEventHandler<VideoRendered>
{
    private readonly ISearchService _searchService;

    public VideoRenderedHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Task HandleAsync(VideoRendered @event, CancellationToken cancellationToken = default)
        => _searchService.AddAsync(new SearchItem(@event.VideoId, ItemKind.Video, @event.Title));
}