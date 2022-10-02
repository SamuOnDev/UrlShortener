using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Services.UserRegister;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbContextClass _context;
        private readonly IUserRegisterService _userRegisterService;

        public UsersController(DbContextClass context, IUserRegisterService userRegisterService)
        {
            _context = context;
            _userRegisterService = userRegisterService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult PostUser(User user)
        {
            if (_userRegisterService.CheckIfUserNameExist(user.UserName))
            {
                return BadRequest("UserName already in Use");
            }
            else if (_userRegisterService.CheckIfEmailExist(user.Email))
            {
                return BadRequest("Email already in Use");
            }

            if (_userRegisterService.RegisterUserToDb(user))
            {
                Console.WriteLine("Usuario Creado");
                return Ok(user);
            }
            else
            {
                return BadRequest("Something went Wrong");
            }           
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
