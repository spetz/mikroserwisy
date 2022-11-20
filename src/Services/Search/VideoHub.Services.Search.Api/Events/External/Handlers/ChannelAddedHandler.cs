using Micro.Handlers;
using VideoHub.Services.Search.Api.Models;
using VideoHub.Services.Search.Api.Services;

namespace VideoHub.Services.Search.Api.Events.External.Handlers;

internal sealed class ChannelAddedHandler : IEventHandler<ChannelAdded>
{
    private readonly ISearchService _searchService;

    public ChannelAddedHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Task HandleAsync(ChannelAdded @event, CancellationToken cancellationToken = default)
        => _searchService.AddAsync(new SearchItem(@event.ChannelId, ItemKind.Channel, @event.Name));
}