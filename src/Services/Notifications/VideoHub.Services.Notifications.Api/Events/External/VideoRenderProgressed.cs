using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Notifications.Api.Events.External;

[Message("videos", "video_render_progressed", "notifications.videos.video_render_progressed")]
public sealed record VideoRenderProgressed(long VideoId, int Progress) : IEvent;