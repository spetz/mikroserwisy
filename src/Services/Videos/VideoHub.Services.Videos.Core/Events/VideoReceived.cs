using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Events;

[Message("videos", "video_received")]
public sealed record VideoReceived(long VideoId, long UserId, string Title) : IEvent;