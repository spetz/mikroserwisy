using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Users.Core.Events;

[Message("users", "signed_in")]
public sealed record SignedIn(long UserId) : IEvent;