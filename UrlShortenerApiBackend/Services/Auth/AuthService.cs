using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using ConfigurationManager = UrlShortenerApiBackend.Manager.ConfigurationManager;

namespace UrlShortenerApiBackend.Services.Auth
{ 
    public class AuthService : IAuthService
    {
        private readonly DbContextClass _context;

        public AuthService(DbContextClass context)
        {
            _context = context;
        }
        JWTTokenResponse IAuthService.UserLoginCorrect(Login login)
        {
            try
            {
                User? userDb = (from user in _context.Users
                                where user.UserName == login.UserName && user.Password == login.Password
                                select user).FirstOrDefault();

                if (userDb is null)
                {
                    return null;
                }

                string token = GenTokenKey();

                return (new JWTTokenResponse
                {
                    Token = token,
                    UserId = userDb.Id
                });

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }

            
        }

        private string GenTokenKey()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(60), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}
