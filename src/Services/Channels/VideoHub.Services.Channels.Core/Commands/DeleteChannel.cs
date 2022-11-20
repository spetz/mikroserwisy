using Micro.Abstractions;

namespace VideoHub.Services.Channels.Core.Commands;

public sealed record DeleteChannel(long ChannelId, long UserId) : ICommand;