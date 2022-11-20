using Micro.Abstractions;

namespace VideoHub.Services.Users.Core.Commands;

public sealed record SignUp(long UserId, string Email, string Username, string Password, string? Role = null) : ICommand;