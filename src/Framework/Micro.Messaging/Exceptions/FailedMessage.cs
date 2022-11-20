using Micro.Abstractions;

namespace Micro.Messaging.Exceptions;

public sealed record FailedMessage(IMessage Message);