using Micro.Abstractions;

namespace VideoHub.Services.Channels.Core.Commands;

public sealed record SubscribeChannel(long ChannelId, long UserId) : ICommand;