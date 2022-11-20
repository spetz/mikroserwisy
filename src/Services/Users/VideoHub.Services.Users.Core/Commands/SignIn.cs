using Micro.Abstractions;

namespace VideoHub.Services.Users.Core.Commands;

public sealed record SignIn(string Email, string Password) : ICommand;