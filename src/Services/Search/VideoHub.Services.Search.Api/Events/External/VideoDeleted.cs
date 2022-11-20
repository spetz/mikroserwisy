using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Search.Api.Events.External;

[Message("videos", "video_deleted", "search.videos.video_deleted")]
public sealed record VideoDeleted(long VideoId) : IEvent;