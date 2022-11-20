using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Events;

[Message("videos", "video_rendered")]
public sealed record VideoRendered(long VideoId, long UserId, string Title) : IEvent;