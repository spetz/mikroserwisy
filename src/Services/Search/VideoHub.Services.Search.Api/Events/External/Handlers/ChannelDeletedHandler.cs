using Micro.Handlers;
using VideoHub.Services.Search.Api.Services;

namespace VideoHub.Services.Search.Api.Events.External.Handlers;

internal sealed class ChannelDeletedHandler : IEventHandler<ChannelDeleted>
{
    private readonly ISearchService _searchService;

    public ChannelDeletedHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Task HandleAsync(ChannelDeleted @event, CancellationToken cancellationToken = default)
        => _searchService.DeleteAsync(@event.ChannelId);
}