using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Services.UrlLists
{
    public interface IUrlListsService
    {
        Task<IEnumerable<UrlList>> GetUrlListByUser(int UserId);
        Task<UrlList> CreateUrlOnDBAsync(UrlDto urlMaster);
        Task<UrlList> DeleteUrlOnDBAsync(int idToDelete);
    }
}
