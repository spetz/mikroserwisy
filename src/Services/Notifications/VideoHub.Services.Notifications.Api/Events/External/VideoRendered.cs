using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Notifications.Api.Events.External;

[Message("videos", "video_rendered", "notifications.videos.video_rendered")]
public sealed record VideoRendered(long VideoId, long UserId, string Title) : IEvent;