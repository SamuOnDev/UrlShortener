using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.UserUrlListService
{
    public interface IUserUrlListService
    {
        Task<UrlList> RegisterUrl(UrlDto url);
    }
}
