namespace VideoHub.Services.Users.Core.DTO;

public record AccountDto(long UserId, string Email, string Username, string Role, DateTime CreatedAt);