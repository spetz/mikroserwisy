using Micro.Abstractions;

namespace VideoHub.Services.Channels.Core.Commands;

public sealed record AddVideoToChannel(long ChannelId, long VideoId, long UserId) : ICommand;