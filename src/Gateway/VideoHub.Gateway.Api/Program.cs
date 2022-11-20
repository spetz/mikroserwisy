using Micro.Framework;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Host
    .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("yarp.json", false));

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetRequiredSection("reverseProxy"));

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.UseMicroFramework()
    .UseEndpoints(x => x.MapReverseProxy());

app.Run();