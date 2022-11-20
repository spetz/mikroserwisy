using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Events;

[Message("videos", "video_render_progressed")]
public sealed record VideoRenderProgressed(long VideoId, int Progress) : IEvent;