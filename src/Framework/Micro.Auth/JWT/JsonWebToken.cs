namespace Micro.Auth.JWT;

public sealed class JsonWebToken
{
    public string AccessToken { get; set; } = string.Empty;
    public long Expiry { get; set; }
    public long UserId { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public IDictionary<string, IEnumerable<string>>? Claims { get; set; } 
}