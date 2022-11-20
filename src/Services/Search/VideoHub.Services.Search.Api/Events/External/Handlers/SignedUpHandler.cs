using Micro.Handlers;
using VideoHub.Services.Search.Api.Models;
using VideoHub.Services.Search.Api.Services;

namespace VideoHub.Services.Search.Api.Events.External.Handlers;

internal sealed class SignedUpHandler : IEventHandler<SignedUp>
{
    private readonly ISearchService _searchService;

    public SignedUpHandler(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
        => _searchService.AddAsync(new SearchItem(@event.UserId, ItemKind.User, @event.Username));
}