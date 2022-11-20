using Micro.API.AsyncApi;
using Saunter.Attributes;
using VideoHub.Services.Users.Core.Commands;
using VideoHub.Services.Users.Core.Events;

namespace VideoHub.Services.Users.Core;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(signed_up), BindingsRef = "users")]
    [SubscribeOperation(typeof(SignedUp), Summary = "User has been created", OperationId = nameof(signed_up))]
    internal abstract void signed_up();
    
    [Channel(nameof(signed_in), BindingsRef = "users")]
    [SubscribeOperation(typeof(SignedIn), Summary = "User has been authenticated", OperationId = nameof(signed_in))]
    internal abstract void signed_in();
    
    [Channel(nameof(user_subscription_updated), BindingsRef = "users")]
    [SubscribeOperation(typeof(UserSubscriptionUpdated), Summary = "User subscription has been updated", OperationId = nameof(user_subscription_updated))]
    internal abstract void user_subscription_updated();
    
    [Channel(nameof(update_user_subscription), BindingsRef = "users")]
    [PublishOperation(typeof(UpdateUserSubscription), Summary = "Update user subscription", OperationId = nameof(update_user_subscription))]
    internal abstract void update_user_subscription();
}