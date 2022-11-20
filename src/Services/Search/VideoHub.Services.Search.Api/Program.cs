using Micro.DAL.Mongo;
using Micro.Framework;
using Micro.Messaging;
using VideoHub.Services.Search.Api.Events.External;
using VideoHub.Services.Search.Api.Models;
using VideoHub.Services.Search.Api.Services;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services
    .AddSingleton<ISearchService, SearchService>()
    .AddMongo(builder.Configuration);

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API").WithName("Info");

app.MapGet("/ping", () => "pong").WithTags("API").WithName("Pong");

app.MapGet("/search", async (string? query, ISearchService service)
        => Results.Ok(await service.SearchAsync(query ?? string.Empty)))
    .WithTags("Search").WithName("Search items");

app.MapPost("items", async (SearchItem item, ISearchService service) =>
    {
        await service.AddAsync(item);
        return Results.NoContent();
    })
    .WithTags("Items").WithName("Add item");

app.MapDelete("items/{itemId:long}", async (long itemId, ISearchService service) =>
    {
        await service.DeleteAsync(itemId);
        return Results.NoContent();
    })
    .WithTags("Items").WithName("Delete item");

app.UseMicroFramework();

app.Subscribe()
    .Event<ChannelAdded>()
    .Event<ChannelDeleted>()
    .Event<SignedUp>()
    .Event<VideoRendered>()
    .Event<VideoDeleted>();

app.Run();