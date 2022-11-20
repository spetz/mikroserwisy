using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Saga.Api.Messages;

[Message("users", "update_user_subscription")]
public record UpdateUserSubscription(long UserId, long LengthLimit) : ICommand;