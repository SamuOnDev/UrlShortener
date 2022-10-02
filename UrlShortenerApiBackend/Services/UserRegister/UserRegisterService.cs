using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.UserRegister
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly DbContextClass _context;

        public UserRegisterService(DbContextClass context)
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

        public bool RegisterUserToDb(User user)
        {
            try
            {
                _context.Users.Add(user);

                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Error Generating the User", exception);
            }
        }
    }
}
