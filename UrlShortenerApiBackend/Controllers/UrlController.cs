using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Services.UrlLists;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("/")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly DbContextClass _context;

        public UrlController(DbContextClass context)
        {
            _context = context;
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetUrlAsync(string shortUrl)
        {
            string path = HttpContext.Request.Path.ToUriComponent().Trim('/');

            UrlList urlInList = _context.UrlLists.Where(x => x.ShortUrl == shortUrl).Single();

            if (urlInList is null)
            {
                return BadRequest("La URL no existe");
            }

            urlInList.UsesCounter = urlInList.UsesCounter + 1;

            _context.Entry(urlInList).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Redirect(urlInList.Url);
        }
    }
}
