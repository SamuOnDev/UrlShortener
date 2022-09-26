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
using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Services.UserUrlListService;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlListsController : ControllerBase
    {
        private readonly UrlShortenerDBContext _context;
        private readonly IUserUrlListService _userUrlListService;

        public UrlListsController(UrlShortenerDBContext context, IUserUrlListService userUrlListService)
        {
            _context = context;
            _userUrlListService = userUrlListService;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<UrlList>> PostUrlList(UrlDto urlDto)
        {
            //var shortUrl = _userUrlListService.RegisterUrl(urlDto);
            var user = HttpContext.User;

            var userId = HttpContext.User.Claims.First(i => i.Type == "Id").Value;

            // TODO: Agregar la funcion para guardar la URL en tu lista con el numero de ID del usuario y el resto de funcion con el Short, para que el return sea el link Short 

            //_context.UrlLists.Add(urlList);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUrlList", new { id = urlList.Id }, urlList);

            return Ok($"Url Creada: {userId}");
        }

        // DELETE: api/UrlLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrlList(int id)
        {
            var urlList = await _context.UrlLists.FindAsync(id);
            if (urlList == null)
            {
                return NotFound();
            }

            _context.UrlLists.Remove(urlList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UrlListExists(int id)
        {
            return _context.UrlLists.Any(e => e.Id == id);
        }
    }
}
