namespace VideoHub.Services.Users.Core.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = new();
    public string RoleId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}