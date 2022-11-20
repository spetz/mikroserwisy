using Micro.Abstractions;
using Micro.Attributes;

namespace VideoHub.Services.Channels.Core.Commands;

[Message("channels", "add_channel", "channels.channels.add_channel")]
public sealed record AddChannel(long ChannelId, long UserId, string Name, string? Description = default) : ICommand;