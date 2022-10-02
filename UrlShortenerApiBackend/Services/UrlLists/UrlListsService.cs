using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.UrlLists
{
    public class UrlListsService : IUrlListsService
    {
        private readonly DbContextClass _context;

        public UrlListsService(DbContextClass context)
        {
            _context = context;
        }
        public async Task<UrlList> CreateUrlOnDBAsync(UrlDto urlMaster)
        {

            UrlList ReadyToList = new UrlList()
            {
                UserId = urlMaster.UserId,
                Title = urlMaster.Title,
                UsesCounter = 0,
                Url = urlMaster.Url
            };

            _context.UrlLists.Add(ReadyToList);

            await _context.SaveChangesAsync();

            string UrlChunk = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ReadyToList.Id));

            ReadyToList.ShortUrl = UrlChunk;

            await _context.SaveChangesAsync();

            return ReadyToList;
        }

        public async Task<UrlList> DeleteUrlOnDBAsync(int idToDelete)
        {
            UrlList? urlToDelete = await _context.UrlLists.FindAsync(idToDelete);

            if (urlToDelete is null)
            {
                return null;
            }

            _context.UrlLists.Remove(urlToDelete);

            await _context.SaveChangesAsync();

            return urlToDelete;

        }

        public async Task<IEnumerable<UrlList>> GetUrlListByUser(int UserId)
        {
            IEnumerable<UrlList> urls = await _context.UrlLists.Where(x => x.UserId == UserId).ToListAsync();

            return urls;
        }

    }
}
