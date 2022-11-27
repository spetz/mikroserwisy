using System.Collections.Concurrent;
using Humanizer;
using Micro.Abstractions;
using Micro.Contexts;
using Micro.Handlers;
using Microsoft.Extensions.Logging;

namespace Micro.Observability.Logging.Decorators;

internal sealed class LoggingEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private readonly IEventHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly ILogger<LoggingEventHandlerDecorator<T>> _logger;

    public LoggingEventHandlerDecorator(IEventHandler<T> handler, IContextProvider contextProvider,
        ILogger<LoggingEventHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _logger = logger;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var name = Names.GetOrAdd(typeof(T), @event.GetType().Name.Underscore());
        _logger.LogInformation("Handling an event: {EventName} [Trace ID: {TraceId}, Activity ID: {ActivityId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}]...",
            name, context.TraceId, context.ActivityId, context.MessageId, context.CausationId, context.UserId ?? string.Empty);
        await _handler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation("Handled an event: {EventName} [Trace ID: {TraceId}, Activity ID: {ActivityId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}]",
            name, context.TraceId, context.ActivityId, context.MessageId, context.CausationId, context.UserId ?? string.Empty);
    }
}