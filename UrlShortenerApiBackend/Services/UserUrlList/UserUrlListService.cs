using UrlShortenerApiBackend.DataAcces;
using UrlShortenerApiBackend.Models.DataModels;
using System.Linq;

namespace UrlShortenerApiBackend.Services.UserUrlListService
{
    public class UserUrlListService : IUserUrlListService
    {
        private readonly UrlShortenerDBContext _context;

        public UserUrlListService(UrlShortenerDBContext context)
        {
            _context = context;
        }

        public Task<UrlList> RegisterUrl(UrlDto url)
        {
            var UrlList = _context.UrlLists.ToList();

            var asg = UrlList.

            var list = UrlList.Id.OrderBy(s => int.Parse(s));
            var result = Enumerable.Range(list.Min(), list.Count).Except(list).First();

        }    
    }     
}
