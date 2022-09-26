namespace UrlShortenerApiBackend.Services.User
{
    public interface IUserService
    {
        bool CheckIfUserNameExist(string UserName);
        bool CheckIfEmailExist(string Email);
    }
}
