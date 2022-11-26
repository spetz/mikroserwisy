using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("users", "signed_in", "saga.users.signed_in")]
public sealed record SignedIn(long UserId) : IEvent;