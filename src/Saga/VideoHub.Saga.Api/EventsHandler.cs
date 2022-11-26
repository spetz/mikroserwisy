using Chronicle;
using Micro.Handlers;
using VideoHub.Saga.Api.Messages;

namespace VideoHub.Saga.Api;

internal sealed class EventsHandler :
    IEventHandler<SignedUp>,
    IEventHandler<SignedIn>
{
    private readonly ISagaCoordinator _sagaCoordinator;

    public EventsHandler(ISagaCoordinator sagaCoordinator)
    {
        _sagaCoordinator = sagaCoordinator;
    }

    public Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(SignedIn @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
}