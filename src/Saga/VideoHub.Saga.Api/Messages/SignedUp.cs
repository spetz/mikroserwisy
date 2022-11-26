using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("users", "signed_up", "saga.users.signed_up")]
public sealed record SignedUp(long UserId, string Username) : IEvent;