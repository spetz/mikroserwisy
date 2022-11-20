using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Search.Api.Events.External;

[Message("videos", "video_rendered", "search.videos.video_rendered")]
public sealed record VideoRendered(long VideoId, string Title) : IEvent;