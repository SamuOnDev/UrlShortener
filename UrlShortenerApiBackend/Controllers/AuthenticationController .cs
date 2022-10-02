using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Services.Auth;


namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DbContextClass _context;
        private readonly IAuthService _authService;

        public AuthenticationController(DbContextClass context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login is null)
            {
                return BadRequest("Wrong Credentials");
            }

            JWTTokenResponse token = _authService.UserLoginCorrect(login);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);

        }
    }
}
