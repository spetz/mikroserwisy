using Micro.Abstractions;

namespace VideoHub.Services.Channels.Core.Commands;

public sealed record UnsubscribeChannel(long ChannelId, long UserId) : ICommand;