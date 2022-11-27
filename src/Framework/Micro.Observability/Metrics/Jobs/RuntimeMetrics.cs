using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;

namespace Micro.Observability.Metrics.Jobs;

[Meter(MetricsKey)]
internal sealed class RuntimeMetrics : BackgroundService
{
    private const string MetricsKey = "runtime";
    private readonly Meter _meter = new(MetricsKey);
    private long _ramUsage;
    private long _threadsCount;
    private readonly ObservableGauge<long> _ramUsageGauge;
    private readonly ObservableGauge<long> _threadsCountGauge;

    public RuntimeMetrics()
    {
        _ramUsageGauge = _meter.CreateObservableGauge("ram", () => _ramUsage);
        _threadsCountGauge = _meter.CreateObservableGauge("threads", () => _threadsCount);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var process = Process.GetCurrentProcess();
            _ramUsage = process.PrivateMemorySize64;
            _threadsCount = process.Threads.Count;
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}