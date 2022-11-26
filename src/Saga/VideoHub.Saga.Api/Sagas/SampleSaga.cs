using Chronicle;
using VideoHub.Saga.Api.Messages;

namespace VideoHub.Saga.Api.Sagas;

internal sealed class SampleSagaData
{
    public string Username { get; set; }
}

internal sealed class SampleSaga : Saga<SampleSagaData>,
    ISagaStartAction<SignedUp>,
    ISagaAction<SignedIn>
{
    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            SignedUp m => m.UserId.ToString(),
            SignedIn m => m.UserId.ToString(),
            _ => throw new InvalidOperationException("Unsupported message")
        };

    public Task HandleAsync(SignedUp message, ISagaContext context)
    {
        Data.Username = message.Username;
        return Task.CompletedTask;
    }

    public Task CompensateAsync(SignedUp message, ISagaContext context)
        => Task.CompletedTask;

    public Task HandleAsync(SignedIn message, ISagaContext context)
        => CompleteAsync();

    public Task CompensateAsync(SignedIn message, ISagaContext context)
        => Task.CompletedTask;
}