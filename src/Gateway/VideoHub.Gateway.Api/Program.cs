using Micro.Framework;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Host
    .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("yarp.json", false));

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("reverseProxy"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(transform =>
        {
            var correlationId = $"{Guid.NewGuid():N}";
            transform.ProxyRequest.Headers.Add("x-correlation-id", correlationId);
            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.UseMicroFramework()
    .UseEndpoints(x => x.MapReverseProxy());

app.Run();