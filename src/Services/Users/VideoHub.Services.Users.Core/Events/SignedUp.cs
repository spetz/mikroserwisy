using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Users.Core.Events;

[Message("users", "signed_up")]
public sealed record SignedUp(long UserId, string Username) : IEvent;