using System.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;

namespace Micro.Observability.Metrics.Jobs;

[Meter(MetricsKey)]
internal sealed class SampleBackgroundMetrics : BackgroundService
{
    private readonly Random _random = new();
    private const string MetricsKey = "bg-metric";
    private readonly Meter _meter = new(MetricsKey);
    private long _randomValue;
    private readonly ObservableGauge<long> _randomGauge;

    public SampleBackgroundMetrics()
    {
        _randomGauge = _meter.CreateObservableGauge("random", () => _randomValue);
    }
 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _randomValue = _random.Next(1, 100);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}