using Micro.Messaging.Brokers;

namespace Micro.Messaging.Exceptions;

public sealed record MessageExceptionPolicy(bool Retry, bool UseDeadLetter, Func<IMessageBroker, Task>? Publish = null);