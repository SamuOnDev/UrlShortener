using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using UrlShortenerApiBackend.Services.UrlLists;

namespace UrlShortenerApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlListController : ControllerBase
    {
        private readonly DbContextClass _context;
        private readonly IUrlListsService _urlListsService;

        public UrlListController(DbContextClass context, IUrlListsService urlListsService)
        {
            _context = context;
            _urlListsService = urlListsService;
        }

        [HttpGet]
        [Route("UrlList")]
        public async Task<ActionResult<IEnumerable<UrlList>>> GetUrlList(int UserId)
        {
            IEnumerable<UrlList> urlList = await _urlListsService.GetUrlListByUser(UserId);

            return Ok(urlList);
        }

        //[HttpGet]
        //[Route("UrlDetail")]
        //public async Task<ActionResult<UrlList>> GetUrlDetails(int urlId)
        //{
        //    UrlList urlDetails = await _context.UrlLists.Where(x => x.Id == urlId).SingleAsync();

        //    if (urlDetails is null)
        //    {
        //        return NotFound();
        //    }

        //    return urlDetails;
        //}

        [HttpPost]
        [Route("CreateUrl")]
        public async Task<ActionResult<UrlList>> PostUrlList(UrlDto urlDto)
        {
            if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var inputUri))
            {
                return BadRequest("Invalid URL format.");
            }

            UrlList urlListed = await _urlListsService.CreateUrlOnDBAsync(urlDto);

            string resultUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{urlListed.ShortUrl}";

            return Ok(resultUrl);
        }

        [HttpDelete]
        [Route("DeleteUrl")]
        public async Task<IActionResult> DeleteUrl(int idToDelete)
        {
            await _urlListsService.DeleteUrlOnDBAsync(idToDelete);

            return NoContent();
        }
    }
}
