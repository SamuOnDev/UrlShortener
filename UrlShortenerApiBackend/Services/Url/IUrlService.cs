namespace UrlShortenerApiBackend.Services.Url
{
    public interface IUrlService
    {
        Task RedirectDelegate(HttpContext httpContext);
    }
}
