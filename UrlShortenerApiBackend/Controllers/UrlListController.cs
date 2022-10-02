using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlListController : ControllerBase
    {
        private readonly DbContextClass _context;

        public UrlListController(DbContextClass context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("url")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<UrlList>> PostUrlList(UrlDto urlDto)
        {
            if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var inputUri))
            {
                return BadRequest("Invalid URL format.");
            }

            int userId = Convert.ToInt32(HttpContext.User.Claims.First(i => i.Type == "Id").Value);

            UrlList ReadyToList = new UrlList()
            {
                Id = _context.UrlLists.Max(x => x.Id) + 1,
                CreatedBy = _context.Users.Where(u => u.Id.Equals(userId)).Select(n => n.UserName).Single(),
                UserId = userId,
                Title = urlDto.Title,
                UsesCounter = 0,
                Url = urlDto.Url
            };

            _context.UrlLists.Add(ReadyToList);

            await _context.SaveChangesAsync();

            string UrlChunk = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ReadyToList.Id));

            ReadyToList.ShortUrl = UrlChunk;

            await _context.SaveChangesAsync();

            string resultUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{ReadyToList.ShortUrl}";

            return Ok(resultUrl);
        }
    }
}
