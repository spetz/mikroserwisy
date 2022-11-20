using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events.External;

[Message("videos", "video_rendered", "channels.videos.video_rendered")]
public sealed record VideoRendered(long VideoId, long UserId, string Title) : IEvent;