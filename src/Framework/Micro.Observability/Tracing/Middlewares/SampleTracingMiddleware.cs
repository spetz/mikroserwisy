using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Micro.Observability.Tracing.Middlewares;

internal sealed class SampleTracingMiddleware : IMiddleware
{
    public const string ActivitySourceName = "sample_tracing";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var requestPath = context.Request.Path.ToString();
        using var activity = ActivitySource.StartActivity("start_tracing");
        activity?.SetTag("path", requestPath);
        await next(context);
    }
}