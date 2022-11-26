using Chronicle;
using Micro.Messaging.Brokers;
using VideoHub.Saga.Api.Messages;

namespace VideoHub.Saga.Api.Sagas;

internal sealed class UserSubscriptionSagaData
{
    public long UserId { get; set; }
    public int SubscribersCount { get; set; }
}

internal sealed class UserSubscriptionSaga : Saga<UserSubscriptionSagaData>,
    ISagaStartAction<ChannelAdded>,
    ISagaAction<ChannelDeleted>,
    ISagaAction<ChannelSubscribed>,
    ISagaAction<ChannelUnsubscribed>
{
    private const int RequiredSubscribers = 2;
    private const int PremiumLengthLimit = 1000;
    private readonly IMessageBroker _messageBroker;

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            ChannelAdded m => m.ChannelId.ToString(),
            ChannelDeleted m => m.ChannelId.ToString(),
            ChannelSubscribed m => m.ChannelId.ToString(),
            ChannelUnsubscribed m => m.ChannelId.ToString(),
            _ => throw new InvalidOperationException($"Unsupported message: {message.GetType().Name}.")
        };

    public UserSubscriptionSaga(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public Task HandleAsync(ChannelAdded message, ISagaContext context)
    {
        Data.UserId = message.UserId;
        return Task.CompletedTask;
    }

    public Task CompensateAsync(ChannelAdded message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(ChannelDeleted message, ISagaContext context)
        => CompleteAsync();

    public Task CompensateAsync(ChannelDeleted message, ISagaContext context)
        => Task.CompletedTask;

    public async Task HandleAsync(ChannelSubscribed message, ISagaContext context)
    {
        Data.SubscribersCount++;
        if (Data.SubscribersCount is not RequiredSubscribers)
        {
            return;
        }

        await _messageBroker.SendAsync(new UpdateUserSubscription(Data.UserId, PremiumLengthLimit));
        await CompleteAsync();
    }

    public Task CompensateAsync(ChannelSubscribed message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(ChannelUnsubscribed message, ISagaContext context)
    {
        Data.SubscribersCount--;
        return Task.CompletedTask;
    }

    public Task CompensateAsync(ChannelUnsubscribed message, ISagaContext context)
        => Task.CompletedTask;
}