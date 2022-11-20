using Micro.Exceptions;

namespace VideoHub.Services.Users.Core.Exceptions;

internal class RoleNotFoundException : CustomException
{
    public string Role { get; }

    public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
    {
        Role = role;
    }
}