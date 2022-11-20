using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Search.Api.Events.External;

[Message("users", "signed_up", "search.users.signed_up")]
public sealed record SignedUp(long UserId, string Username) : IEvent;