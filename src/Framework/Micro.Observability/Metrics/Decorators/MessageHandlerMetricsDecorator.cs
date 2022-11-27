using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using Humanizer;
using Micro.Abstractions;
using Micro.Messaging.RabbitMQ.Internals;

namespace Micro.Observability.Metrics.Decorators;

[Meter(MetricsKey)]
internal sealed class MessageHandlerMetricsDecorator : IMessageHandler
{
    private readonly IMessageHandler _messageHandler;
    private const string MetricsKey = "message_handler";
    private static readonly ConcurrentDictionary<Type, string> Names = new();
    private static readonly Meter Meter = new(MetricsKey);
    private static readonly Counter<long> ReceivedMessagesCounter = Meter.CreateCounter<long>("received_messages");

    public MessageHandlerMetricsDecorator(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    public Task HandleAsync<T>(Func<IServiceProvider, T, CancellationToken, Task> handler, T message,
        CancellationToken cancellationToken = default) where T : IMessage
    {
        var name = Names.GetOrAdd(typeof(T), message.GetType().Name.Underscore());
        var tags = new KeyValuePair<string, object?>[]
        {
            new("message", name)
        };

        ReceivedMessagesCounter.Add(1, tags);
        return _messageHandler.HandleAsync(handler, message, cancellationToken);
    }
}