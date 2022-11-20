using Micro.Abstractions;

namespace VideoHub.Services.Videos.Core.Commands;

public sealed record DeleteVideo(long VideoId, long UserId) : ICommand;