using System.Collections.Concurrent;
using Micro.Abstractions;
using Micro.Messaging.Streams;
using Micro.Messaging.Streams.Serialization;
using Microsoft.Extensions.Logging;
using RabbitMQ.Stream.Client;

namespace Micro.Messaging.RabbitMQ.Streams.Publishers;

internal sealed class RabbitMQStreamPublisher : IStreamPublisher
{
    private readonly RabbitStreamManager _streamManager;
    private readonly IStreamSerializer _serializer;
    private readonly ConcurrentDictionary<string, ProducerDetails> _producers = new();
    private readonly ILogger<RabbitMQStreamPublisher> _logger;

    public RabbitMQStreamPublisher(RabbitStreamManager streamManager, IStreamSerializer serializer,
        ILogger<RabbitMQStreamPublisher> logger)
    {
        _streamManager = streamManager;
        _serializer = serializer;
        _logger = logger;
    }

    public async Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        if (!_producers.TryGetValue(stream, out var producerDetails))
        {
            var producer = await _streamManager.CreateProducerAsync(stream);
            var lastPublishingId = await producer.GetLastPublishingId();
            producerDetails = new ProducerDetails(producer, lastPublishingId);
            _producers.TryAdd(stream, producerDetails);
            _logger.LogInformation($"Created a new producer for stream: '{stream}'.");
        }

        var payload = new Message(_serializer.Serialize(message));
        var publishingId = await _producers[stream].SendAsync(payload);
        _logger.LogInformation($"Sent a message with publishing ID: {publishingId} [stream: '{stream}'].");
    }

    private class ProducerDetails
    {
        private readonly Producer _producer;
        private ulong _publishingId;

        public ProducerDetails(Producer producer, ulong publishingId)
        {
            _producer = producer;
            _publishingId = publishingId;
        }

        public async ValueTask<ulong> SendAsync(Message message)
        {
            var publishingId = Interlocked.Increment(ref _publishingId);
            await _producer.Send(publishingId, message);
            return publishingId;
        }
    }
}