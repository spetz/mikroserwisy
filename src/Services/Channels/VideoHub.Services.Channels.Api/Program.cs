using Micro.Auth;
using Micro.Framework;
using Micro.Handlers;
using Micro.Identity;
using Micro.Messaging;
using VideoHub.Services.Channels.Core;
using VideoHub.Services.Channels.Core.Commands;
using VideoHub.Services.Channels.Core.Events.External;
using VideoHub.Services.Channels.Core.Queries;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services.AddCore(builder.Configuration);

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.MapGet("/channels/{channelId:long}", async (long channelId, IDispatcher dispatcher) =>
{
    var channel = await dispatcher.QueryAsync(new GetChannel(channelId));
    return channel is null ? Results.NotFound() : Results.Ok(channel);
}).WithTags("Channels").WithName("Get channel");

app.MapGet("/channels", async (string? name, long? userId, IDispatcher dispatcher)
        => Results.Ok(await dispatcher.QueryAsync(new GetChannels(name, userId))))
    .WithTags("Channels").WithName("Get channels");

app.MapPost("/channels", async (AddChannel command, HttpContext context, IDispatcher dispatcher, IIdGen idGen) =>
{
    command = command with {ChannelId = idGen.Create(), UserId = context.UserId()};
    await dispatcher.SendAsync(command);
    return Results.CreatedAtRoute("Get channel", new {command.ChannelId});
}).WithTags("Channels").WithName("Add channel").RequireAuthorization();

app.MapDelete("/channels/{channelId:long}", async (long channelId, HttpContext context, IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(new DeleteChannel(channelId, context.UserId()));
    return Results.NoContent();
}).WithTags("Channels").WithName("Delete channel").RequireAuthorization();

app.MapPost("/channels/{channelId:long}/videos/{videoId:long}", 
    async (long channelId, long videoId, HttpContext context, IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(new AddVideoToChannel(channelId, videoId, context.UserId()));
    return Results.NoContent();
}).WithTags("Videos").WithName("Add video").RequireAuthorization();

app.MapDelete("/channels/{channelId:long}/videos/{videoId:long}",
    async (long channelId, long videoId, HttpContext context, IDispatcher dispatcher) =>
    {
        await dispatcher.SendAsync(new DeleteVideoFromChannel(channelId, videoId, context.UserId()));
        return Results.NoContent();
    }).WithTags("Videos").WithName("Delete video").RequireAuthorization();

app.MapPost("/channels/{channelId:long}/subscriptions",
    async (long channelId, HttpContext context, IDispatcher dispatcher) =>
    {
        await dispatcher.SendAsync(new SubscribeChannel(channelId, context.UserId()));
        return Results.NoContent();
    }).WithTags("Subscriptions").WithName("Subscribe channel").RequireAuthorization();

app.MapDelete("/channels/{channelId:long}/subscriptions",
    async (long channelId, HttpContext context, IDispatcher dispatcher) =>
    {
        await dispatcher.SendAsync(new UnsubscribeChannel(channelId, context.UserId()));
        return Results.NoContent();
    }).WithTags("Subscriptions").WithName("Unsubscribe channel").RequireAuthorization();

app.UseMicroFramework();

app.Subscribe()
    .Command<AddChannel>()
    .Event<SignedUp>()
    .Event<VideoRendered>()
    .Event<VideoDeleted>();

app.Run();