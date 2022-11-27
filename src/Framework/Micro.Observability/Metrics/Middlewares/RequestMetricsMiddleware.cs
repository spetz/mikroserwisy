using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;

namespace Micro.Observability.Metrics.Middlewares;

[Meter(MetricsKey)]
internal sealed class RequestMetricsMiddleware : IMiddleware
{
    private const string MetricsKey = "requests";
    private static readonly Meter Meter = new(MetricsKey);
    private static readonly Counter<long> SuccessfulRequestsCounter = Meter.CreateCounter<long>("successful");
    private static readonly Counter<long> FailedRequestsCounter = Meter.CreateCounter<long>("failed");

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var tags = new KeyValuePair<string, object?>[]
        {
            new("http_method", context.Request.Method)
        };
        
        try
        {
            await next(context);
            SuccessfulRequestsCounter.Add(1, tags);
        }
        catch
        {
            FailedRequestsCounter.Add(1, tags);
            throw;
        }
    }
}