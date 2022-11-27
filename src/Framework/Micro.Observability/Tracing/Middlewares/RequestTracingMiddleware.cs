using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Micro.Observability.Tracing.Middlewares;

internal sealed class RequestTracingMiddleware : IMiddleware
{
    public const string ActivitySourceName = "request_tracing";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using var activity = ActivitySource.StartActivity("incoming_request");
        activity?.SetTag("path", context.Request.Path);
        activity?.SetTag("method", context.Request.Method);
        activity?.SetTag("trace_id", context.TraceIdentifier);

        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            activity?.SetStatus(ActivityStatusCode.Error, exception.ToString());
            throw;
        }
        finally
        {
            stopwatch.Stop();
            activity?.SetTag("duration", stopwatch.Elapsed);
        }
    }
}