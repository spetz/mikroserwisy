using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Users.Core.Commands;

[Message("users", "update_user_subscription", "users.users.update_user_subscription")]
public record UpdateUserSubscription(long UserId, long? SizeLimit = default, long? VideosLimit = default, long? LengthLimit = default) : ICommand;