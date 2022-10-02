using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.UserRegister
{
    public interface IUserRegisterService
    {
        bool CheckIfUserNameExist(string UserName);
        bool CheckIfEmailExist(string Email);
        bool RegisterUserToDb(User user);
    }
}