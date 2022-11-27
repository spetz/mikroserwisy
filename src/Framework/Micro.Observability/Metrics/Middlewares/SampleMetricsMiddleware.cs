using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;

namespace Micro.Observability.Metrics.Middlewares;

[Meter(MetricsKey)]
internal sealed class SampleMetricsMiddleware : IMiddleware
{
    private const string MetricsKey = "requests-counter";
    private static readonly Meter Meter = new(MetricsKey);
    private static readonly Counter<long> RequestsCounter = Meter.CreateCounter<long>("requests_count");

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        RequestsCounter.Add(1);
        await next(context);
    }
}