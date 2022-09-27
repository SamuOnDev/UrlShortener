using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Helpers;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UrlShortenerDBContext _context; 
        private readonly JwtSettings _jwtSettings;

        public AccountController(UrlShortenerDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var searchUser = (from user in _context.Users
                                  // TODO: Change user.Name for user.UserName
                                  where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                  select user).FirstOrDefault();

                if (searchUser != null)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        UserRole = searchUser.UserRole,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Credentials");
                }

                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            var searchAllUsers = from user in _context.Users select user;

            return Ok(searchAllUsers);
        }
    }
}

    


    
