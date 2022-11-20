using Micro.Abstractions;

namespace VideoHub.Services.Channels.Core.Commands;

public sealed record DeleteVideoFromChannel(long ChannelId, long VideoId, long UserId) : ICommand;