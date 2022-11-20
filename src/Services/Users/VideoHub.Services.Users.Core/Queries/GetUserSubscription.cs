using Micro.Abstractions;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries;

public sealed record GetUserSubscription(long UserId) : IQuery<UserSubscriptionDto?>;