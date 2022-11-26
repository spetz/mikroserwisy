using Chronicle;
using Micro.Handlers;
using VideoHub.Saga.Api.Messages;

namespace VideoHub.Saga.Api;

internal sealed class EventsHandler :
    IEventHandler<ChannelAdded>,
    IEventHandler<ChannelSubscribed>,
    IEventHandler<ChannelUnsubscribed>,
    IEventHandler<ChannelDeleted>
{
    private readonly ISagaCoordinator _sagaCoordinator;

    public EventsHandler(ISagaCoordinator sagaCoordinator)
    {
        _sagaCoordinator = sagaCoordinator;
    }

    public Task HandleAsync(ChannelAdded @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
    
    public Task HandleAsync(ChannelSubscribed @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
    
    public Task HandleAsync(ChannelUnsubscribed @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
    
    public Task HandleAsync(ChannelDeleted @event, CancellationToken cancellationToken = default)
        => _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
}