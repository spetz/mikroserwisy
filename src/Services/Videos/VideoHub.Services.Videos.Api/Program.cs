using Micro.Auth;
using Micro.Framework;
using Micro.Handlers;
using Micro.Identity;
using Micro.Messaging;
using VideoHub.Services.Videos.Core;
using VideoHub.Services.Videos.Core.Commands;
using VideoHub.Services.Videos.Core.Events.External;
using VideoHub.Services.Videos.Core.Queries;
using VideoHub.Services.Videos.Infrastructure;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services
    .AddCore(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.MapGet("/videos/{videoId:long}", async (long videoId, IDispatcher dispatcher) =>
{
    var video = await dispatcher.QueryAsync(new GetVideo(videoId));
    return video is null ? Results.NotFound() : Results.Ok(video);
}).WithTags("Videos").WithName("Get video");

app.MapGet("/videos", async (string? title, long? userId, IDispatcher dispatcher)
        => Results.Ok(await dispatcher.QueryAsync(new GetVideos(title, userId))))
    .WithTags("Videos").WithName("Get videos");

app.MapPost("/videos", async (UploadVideo command, HttpContext context, IDispatcher dispatcher, IIdGen idGen) =>
{
    command = command with {VideoId = idGen.Create(), UserId = context.UserId()};
    await dispatcher.SendAsync(command);
    return Results.CreatedAtRoute("Get video", new {command.VideoId});
}).RequireAuthorization().WithTags("Videos").WithName("Upload video");

app.MapDelete("/videos/{videoId:long}", async (long videoId, HttpContext context, IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(new DeleteVideo(videoId, context.UserId()));
    return Results.NoContent();
}).RequireAuthorization().WithTags("Videos").WithName("Delete video");

app.UseMicroFramework();

app.Subscribe()
    .Command<UploadVideo>()
    .Event<UserSubscriptionUpdated>();

app.Run();
