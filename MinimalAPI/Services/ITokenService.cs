using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);

    }
}
