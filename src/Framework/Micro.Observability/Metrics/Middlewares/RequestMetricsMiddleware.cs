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
        try
        {
            await next(context);
            SuccessfulRequestsCounter.Add(1);
        }
        catch
        {
            FailedRequestsCounter.Add(1);
            throw;
        }
    }
}