using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Videos.Core.Commands;

[Message("videos", "upload_video", "videos.videos.upload_video")]
public sealed record UploadVideo(long VideoId, long UserId, string Title) : ICommand;