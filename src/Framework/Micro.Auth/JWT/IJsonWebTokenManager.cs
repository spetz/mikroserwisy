namespace Micro.Auth.JWT;

public interface IJsonWebTokenManager
{
    JsonWebToken CreateToken(long userId, string? email = null, string? role = null,
        IDictionary<string, IEnumerable<string>>? claims = null);
}