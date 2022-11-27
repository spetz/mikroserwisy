using System.Collections.Concurrent;
using Humanizer;
using Micro.Abstractions;
using Micro.Contexts;
using Micro.Handlers;
using Microsoft.Extensions.Logging;

namespace Micro.Observability.Logging.Decorators;

internal sealed class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private readonly ICommandHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<T> handler, IContextProvider contextProvider,
        ILogger<LoggingCommandHandlerDecorator<T>> logger)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _logger = logger;
    }

    public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var name = Names.GetOrAdd(typeof(T), command.GetType().Name.Underscore());
        _logger.LogInformation("Handling a command: {CommandName} [Trace ID: {TraceId}, Activity ID: {ActivityId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}']...",
            name, context.TraceId, context.ActivityId, context.MessageId, context.CausationId, context.UserId ?? string.Empty);
        await _handler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command: {CommandName} [Trace ID: {TraceId}, Activity ID: {ActivityId}, Message ID: {MessageId}, Causation ID: {CausationId}, User ID: {UserId}]",
            name, context.TraceId, context.ActivityId, context.MessageId, context.CausationId, context.UserId ?? string.Empty);
    }
}