using UrlShortenerApiBackend.DataAcces;

namespace UrlShortenerApiBackend.Services.User
{
    public class UserService : IUserService
    {
        private readonly UrlShortenerDBContext _context;

        public UserService(UrlShortenerDBContext context)
        {
            _context = context;
        }

        public bool CheckIfUserNameExist(string UserName)
        {

            var UserNameFound = (from user in _context.Users
                                 where user.UserName == UserName
                                 select user).FirstOrDefault();

            if (UserNameFound == null)
            {
                return false;
            }

            return true;
        }

        public bool CheckIfEmailExist(string Email)
        {
            var EmailFound = (from user in _context.Users
                              where user.Email.Equals(Email)
                              select user).FirstOrDefault();

            if (EmailFound == null)
            {
                return false;
            }

            return true;
        }


    }
}
