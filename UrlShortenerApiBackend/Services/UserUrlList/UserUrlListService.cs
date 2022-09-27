//using UrlShortenerApiBackend.DataAcces;
//using UrlShortenerApiBackend.Models.DataModels;
//using System.Linq;
//using Microsoft.AspNetCore.WebUtilities;

//namespace UrlShortenerApiBackend.Services.UserUrlListService
//{
//    public class UserUrlListService : IUserUrlListService
//    {
//        private readonly UrlShortenerDBContext _context;

//        public UserUrlListService(UrlShortenerDBContext context)
//        {
//            _context = context;
//        }

//        public Task<UrlList> ConvertUrl(UrlDto urlDto, int Id)
//        {                        
//            var UrlChunk = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(Id));

//            var ReadyToList = new UrlList()
//            {
//                UserId = Id,
//                Title = "Titulo de prueba",
//                UsesCounter = 0,
//                Url = urlDto.Url,
//                ShortUrl = UrlChunk
//            };

//            return Task.FromResult(ReadyToList);
//        }
//    }     
//}
