using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Events;

[Message("videos", "video_deleted")]
public sealed record VideoDeleted(long VideoId) : IEvent;