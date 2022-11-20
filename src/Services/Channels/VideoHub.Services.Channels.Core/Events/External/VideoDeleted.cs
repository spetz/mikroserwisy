using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Events.External;

[Message("videos", "video_deleted", "channels.videos.video_deleted")]
public sealed record VideoDeleted(long VideoId) : IEvent;