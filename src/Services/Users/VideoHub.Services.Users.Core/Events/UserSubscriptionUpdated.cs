using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Users.Core.Events;

[Message("users", "user_subscription_updated")]
public sealed record UserSubscriptionUpdated(int Version, long UserId, long SizeLimit, long VideosLimit, long LengthLimit) : IEvent;