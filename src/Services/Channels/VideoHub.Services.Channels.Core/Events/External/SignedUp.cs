using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events.External;

[Message("users", "signed_up", "channels.users.signed_up")]
public sealed record SignedUp(long UserId, string Username) : IEvent;