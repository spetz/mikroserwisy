using System.Diagnostics;

namespace Micro.Contexts;

public sealed class Context : IContext
{
    public string ActivityId { get; }
    public string? TraceId { get; }
    public string? MessageId { get; }
    public string? CausationId { get; }
    public string? UserId { get; }

    public Context()
    {
        ActivityId = Activity.Current?.Id ?? ActivityTraceId.CreateRandom().ToString();
        TraceId = string.Empty;
    }

    public Context(string activityId, string? traceId = default, string? messageId = default,
        string? causationId = default, string? userId = default)
    {
        ActivityId = activityId;
        TraceId = traceId;
        MessageId = messageId;
        CausationId = causationId;
        UserId = userId;
    }
}