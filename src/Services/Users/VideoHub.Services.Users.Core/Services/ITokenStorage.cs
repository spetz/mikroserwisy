using Micro.Auth.JWT;

namespace VideoHub.Services.Users.Core.Services;

public interface ITokenStorage
{
    void Set(JsonWebToken jwt);
    JsonWebToken? Get();
}