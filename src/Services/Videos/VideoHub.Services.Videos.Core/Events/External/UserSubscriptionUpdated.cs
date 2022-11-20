using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Events.External;

[Message("users", "user_subscription_updated", "videos.users.user_subscription_updated")]
public sealed record UserSubscriptionUpdated(int Version, long UserId, long SizeLimit, long VideosLimit, long LengthLimit) : IEvent;