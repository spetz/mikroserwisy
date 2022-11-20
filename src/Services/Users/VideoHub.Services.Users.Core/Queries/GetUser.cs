using Micro.Abstractions;
using VideoHub.Services.Users.Core.DTO;

namespace VideoHub.Services.Users.Core.Queries;

public sealed record GetUser(long UserId) : IQuery<UserDto?>;