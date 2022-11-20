using Micro.Framework;
using Micro.Messaging;
using VideoHub.Services.Notifications.Api.Events.External;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.UseMicroFramework();

app.Subscribe()
    .Event<VideoRenderProgressed>()
    .Event<VideoRendered>();

app.Run();