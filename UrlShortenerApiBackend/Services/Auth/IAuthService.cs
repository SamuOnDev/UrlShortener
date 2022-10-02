using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.Auth
{
    public interface IAuthService
    {
        JWTTokenResponse UserLoginCorrect(Login login);
    }
}
