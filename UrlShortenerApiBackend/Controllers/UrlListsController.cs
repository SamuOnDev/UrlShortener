using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class UrlListsController : ControllerBase
    {
        private readonly UrlShortenerDBContext _context;

        public UrlListsController(UrlShortenerDBContext context)
        {
            _context = context;
        }

        // GET: api/UrlLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlList>>> GetUrlLists()
        {
            return await _context.UrlLists.ToListAsync();
        }

        // GET: api/UrlLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UrlList>> GetUrlList(int id)
        {
            var urlList = await _context.UrlLists.FindAsync(id);

            if (urlList == null)
            {
                return NotFound();
            }

            return urlList;
        }

        // PUT: api/UrlLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrlList(int id, UrlList urlList)
        {
            if (id != urlList.Id)
            {
                return BadRequest();
            }

            _context.Entry(urlList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UrlLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/UrlLists/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> DeleteUrlList(int id)
        {
            int userId = Convert.ToInt32(HttpContext.User.Claims.First(i => i.Type == "Id").Value);

            UrlList? urlList = await _context.UrlLists.FindAsync(id);

            if (urlList == null)
            {
                return NotFound();
            }

            if (urlList.UserId == userId)
            {
                _context.UrlLists.Remove(urlList);
                await _context.SaveChangesAsync();

                return Ok("URL Deleted");
            }

            return BadRequest("The user not match");
        }

        private bool UrlListExists(int id)
        {
            return _context.UrlLists.Any(e => e.Id == id);
        }
    }
}
